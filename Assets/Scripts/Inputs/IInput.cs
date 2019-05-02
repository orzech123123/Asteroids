using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inputs
{
    public interface IInput
    {
        IEnumerable<RuntimePlatform> Platforms { get; }
        float GetCameraRotateAxis();
        float GetForwardMove();
        float GetSideMove();
        bool GetJump();
    }
}