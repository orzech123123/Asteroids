using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inputs
{
    public class WindowsInput : IInput
    {
        public IEnumerable<RuntimePlatform> Platforms
        {
            get
            {
                yield return RuntimePlatform.WindowsPlayer;
                yield return RuntimePlatform.WindowsEditor;
                yield return RuntimePlatform.LinuxPlayer;
                yield return RuntimePlatform.LinuxEditor;
            }
        }

        public float GetCameraRotateAxis()
        {
            return Input.GetAxisRaw("Mouse X");
        }

        public float GetForwardMove()
        {
            return Input.GetAxis("Vertical");
        }

        public float GetSideMove()
        {
            return Input.GetAxis("Horizontal");
        }

        public bool GetJump()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}