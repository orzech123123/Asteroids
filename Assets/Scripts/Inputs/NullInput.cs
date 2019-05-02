using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inputs
{
    public class NullInput : IInput
    {
        public IEnumerable<RuntimePlatform> Platforms
        {
            get
            {
                yield break;
            }
        }

        public float GetCameraRotateAxis()
        {
            return 0f;
        }

        public float GetForwardMove()
        {
            return 0f;
        }

        public float GetSideMove()
        {
            return 0f;
        }

        public bool GetJump()
        {
            return false;
        }
    }
}