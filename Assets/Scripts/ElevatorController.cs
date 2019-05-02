using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class ElevatorController : MonoBehaviour
    {
        Vector3 _startPosition;
        Vector3 _targetPosition;
        float _timeToReachTarget;
        float _t;

        private float _yOffset = 40f;
        private float _time = 15;


        void Start()
        {
            _startPosition = transform.position;
            _targetPosition = new Vector3(_startPosition.x, _startPosition.y + _yOffset * GetZeroOROne(), _startPosition.z);

            ResetClock();
        }

        private int GetZeroOROne()
        {
            var result = Random.Range(-1, 2);

            if (result == 0)
            {
                return GetZeroOROne();
            }

            return result;
        }

        void FixedUpdate()
        {
            _t += Time.deltaTime / _timeToReachTarget;
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, _t);

            if (transform.position == _targetPosition)
            {
                var tmpPosition = _startPosition;
                _startPosition = _targetPosition;
                _targetPosition = tmpPosition;

                ResetClock();
            }
        }

        public void ResetClock()
        {
            _t = 0;
            _timeToReachTarget = _time;
        }
    }
}
