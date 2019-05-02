using UnityEngine;

namespace Assets.Scripts
{
    public class ApplicationManager
    {
        public RuntimePlatform Platform
        {
            get { return Application.platform; }
        }
    }
}