using UnityEngine;
using Zenject;

namespace Assets.Scripts.Inputs
{
    public class InputListener : ITickable
    {
        private InputManager _inputManager;

        [Inject]
        public void Construct(InputManager inputManager)
        {
            _inputManager = inputManager;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _inputManager.Pause();
            }

            if (Input.GetKeyUp(KeyCode.P))
            {
                _inputManager.Play();
            }
        }
    }
}