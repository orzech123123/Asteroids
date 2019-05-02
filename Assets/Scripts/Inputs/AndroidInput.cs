using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree.Util;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Inputs
{
    public class AndroidInput : IInput
    {
        private AndroidInputCanvasController _controller;
        private bool _jump;
        private int? _lookAroundFingerId;
        private int? _moveFingerId;

        [Inject]
        public void Construct(AndroidInputCanvasController controller)
        {
            _controller = controller;

            //_controller.RegisterOnJumpButtonPointerDown(() => { _jump = true; }); //TODO odkomentuj
        }

        public IEnumerable<RuntimePlatform> Platforms
        {
            get
            {
                yield return RuntimePlatform.Android;
            }
        }

        public float GetCameraRotateAxis()
        {
            return GetTouchDeltaPosition(ref _lookAroundFingerId, p => p.x > Screen.width / 2).x;
        }

        public float GetForwardMove()
        {
            return GetTouchDeltaPosition(ref _moveFingerId, p => p.x < Screen.width / 2).y;
        }

        public float GetSideMove()
        {
            return GetTouchDeltaPosition(ref _moveFingerId, p => p.x < Screen.width / 2).x;
        }

        public bool GetJump()
        {
            if (!_jump)
            {
                return false;
            }

            _jump = false;
            return true;
        }

        private Vector3 GetTouchDeltaPosition(ref int? fingerId, Func<Vector3, bool> positionLimitation)
        {
            if (!fingerId.HasValue)
            {
                var beganTouches = Input.touches
                    .Where(t => t.phase == TouchPhase.Began)
                    .Where(t => positionLimitation(t.position));

                if (beganTouches.Any())
                {
                    fingerId = beganTouches.First().fingerId;
                }

                return Vector3.zero;
            }

            var tmpFingerId = fingerId;
            var touch = Input.touches.Single(t => t.fingerId == tmpFingerId);

            if (touch.phase == TouchPhase.Ended)
            {
                fingerId = null;
                return Vector3.zero;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                return touch.deltaPosition.normalized;
            }

            return Vector3.zero;
        }
    }
}