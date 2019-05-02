using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Inputs
{
    public class InputManager : ITickable
    {
        private IInput _currentInput;
        private IInput _pauseInput;
        private IInput _platformInput;
        private IEnumerable<IInput> _inputs;

        private float _cameraRotateAxis;
        private float _forwardMove;
        private float _sideMove;
        private bool _jump;

        public InputManager(IEnumerable<IInput> inputs, NullInput nullInput, ApplicationManager applicationManager)
        {
            _pauseInput = nullInput;
            _inputs = inputs;

            _currentInput = _platformInput = GetInputByPlatform(applicationManager.Platform);
        }

        public float GetCameraRotateAxis()
        {
            return _cameraRotateAxis;
        }

        public float GetForwardMove()
        {
            return _forwardMove;
        }

        public float GetSideMove()
        {
            return _sideMove;
        }

        public bool GetJump()
        {
            var result = _jump;
            _jump = false;

            return result;
        }

        public void Pause()
        {
            _platformInput = _currentInput;
            _currentInput = _pauseInput;
        }

        public void Play()
        {
            _currentInput = _platformInput;
        }

        public IInput GetInputByPlatform(RuntimePlatform platform)
        {
            return _inputs.First(i => i.Platforms.Contains(platform));
        }

        public void Tick()
        {
            _cameraRotateAxis = _currentInput.GetCameraRotateAxis();
            _forwardMove = _currentInput.GetForwardMove();
            _sideMove = _currentInput.GetSideMove();

            if (_currentInput.GetJump())
            {
                _jump = true;
            }
        }
    }
}
