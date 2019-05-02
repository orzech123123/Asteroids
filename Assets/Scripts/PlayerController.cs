using Assets.Scripts.Inputs;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class PlayerController : IFixedTickable
    {
        private float _toCameraAdjustSpeed = 10f;
        private float _speed = 5f;

        private readonly CameraController _cameraController;
        private readonly InputManager _inputManager;

        private readonly GameObject _playerGo;
        private readonly GameObject _playerMeshGo;

        private readonly Rigidbody _rigidBody;

        private bool _isMoving;
        private bool _isGoingUp;
        private bool _isFalling;
        private bool _isGrounded;
        private bool _hasJumped;
        private Vector3 _lastPosition;

        public PlayerController(
            InputManager inputManager,
            CameraController cameraController,
            GameObject playerGo,
            GameObject playerMeshGo)
        {
            _inputManager = inputManager;
            _cameraController = cameraController;
            _playerGo = playerGo;
            _playerMeshGo = playerMeshGo;
            //_rigidBody = playerGo.GetComponent<Rigidbody>(); //TODO odkomentuj

            //_lastPosition = playerGo.transform.position;
        }

        public void FixedTick()
        {
            Debug.Log("Moving: " + _isMoving + "; GoingUp: " + _isGoingUp + "; Falling: " + _isFalling + "; Grounded: " + _isGrounded);

            if (_inputManager.GetForwardMove() != 0f || _inputManager.GetSideMove() != 0f)
            {
                AdjustPlayerRotationToCamera();
                MovePlayer();
            }

            if (_inputManager.GetJump() && _isGrounded)
            {
                _hasJumped = true;
                Jump();
            }

            UpdateMovementStates();
            RunAnimations();
        }

        private void RunAnimations()
        {
            if (_isMoving && _isGrounded)
            {
                //_playerMeshGo.GetComponent<Actions>().Walk();
            }
            else if (_hasJumped)
            {
                _hasJumped = false;
                //_playerMeshGo.GetComponent<Actions>().Jump();
            }
            else
            {
                //_playerMeshGo.GetComponent<Actions>().Stay();
            }
        }

        private void UpdateMovementStates()
        {
            _isMoving = new Vector2(_playerGo.transform.position.x, _playerGo.transform.position.z) != new Vector2(_lastPosition.x, _lastPosition.z);
            _isGoingUp = _playerGo.transform.position.y > _lastPosition.y;
            _isFalling = _playerGo.transform.position.y < _lastPosition.y;
            _isGrounded = Physics.Raycast(_playerGo.transform.position, Vector3.down, 1.26f);

            _lastPosition = _playerGo.transform.position;
        }

        private void Jump()
        {
            _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, 0, _rigidBody.velocity.z);
            _rigidBody.AddForce(0, 12f, 0, ForceMode.Impulse);
        }

        private void MovePlayer()
        {
            var cameraForward = _cameraController.ForwardVector.normalized;
            var cameraSide = _cameraController.SideVector.normalized;
            var forward = _inputManager.GetForwardMove();
            var side = _inputManager.GetSideMove();

            var forwardX = cameraForward.x * forward;
            var forwardZ = cameraForward.z * forward;
            var sideX = cameraSide.x * side;
            var sideZ = cameraSide.z * side;

            var result = (new Vector2(forwardX, forwardZ) + new Vector2(sideX, sideZ)).normalized * _speed;

            //            _rigidBody.velocity = new Vector3(result.x, _rigidBody.velocity.y, result.y);
            _playerGo.transform.position += new Vector3(result.x, 0, result.y) * Time.deltaTime;
        }

        private void AdjustPlayerRotationToCamera()
        {
            var originalRotation = _playerMeshGo.transform.rotation.eulerAngles;

            var targetDir = _cameraController.ForwardVector;
            float step = _toCameraAdjustSpeed * Time.deltaTime;
            var newDir = Vector3.RotateTowards(_playerMeshGo.transform.forward, targetDir, step, 0.0F);

            var rotation = Quaternion.LookRotation(newDir);

            _playerMeshGo.transform.rotation = Quaternion.Euler(originalRotation.x, rotation.eulerAngles.y, originalRotation.z);
        }
    }
}