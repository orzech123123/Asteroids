using Assets.Scripts.Inputs;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class CameraController : ITickable, IInitializable
    {
        private InputManager _inputManager;

        private GameObject _cameraGo;
        private GameObject _targetGo;

        private Camera _camera;
        private Vector3 _cameraOffset;

        [Inject]
        public void Construct(InputManager inputManager, GameObject cameraGo, GameObject targetGo)
        {
            _inputManager = inputManager;
            _cameraGo = cameraGo;
            _targetGo = targetGo;
        }

        public Vector3 ForwardVector
        {
            get { return _camera.transform.forward; }
        }

        public Vector3 SideVector
        {
            get { return _camera.transform.right; }
        }

        public void Enable()
        {
            _camera.enabled = true;
        }

        public void Disable()
        {
            _camera.enabled = false;
        }

        public void Tick()
        {
            _cameraGo.transform.RotateAround(_targetGo.transform.position, Vector3.up, _inputManager.GetCameraRotateAxis() * 4);
            _cameraGo.transform.position = new Vector3(_cameraGo.transform.position.x, _targetGo.transform.position.y + _cameraOffset.y, _cameraGo.transform.position.z);
        }

        public void Initialize()
        {
            _camera = _cameraGo.GetComponent<Camera>();
            _cameraOffset = _cameraGo.transform.position - _targetGo.transform.position;
        }
    }
}