using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private Camera _mainCamera;
        private CameraController _cameraController;

        [Inject]
        public void Construct(CameraController cameraController)
        {
            _cameraController = cameraController;
        }

        void Start()
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            StartCoroutine(RunCoroutine());
        }

        IEnumerator RunCoroutine()
        {
            yield return new WaitForSeconds(2f);

            _mainCamera.enabled = false;
            _cameraController.Enable();
        }
    }
}