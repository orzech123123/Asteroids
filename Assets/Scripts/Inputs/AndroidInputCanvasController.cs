using System;
using Assets.Scripts.Ui;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Inputs
{
    public class AndroidInputCanvasController : IInitializable
    {
        private GameObject _gameObject;
        private ExtraButton _jumpButton;
        private ApplicationManager _applicationManager;

        public bool IsMoveForwardButtonPressed { get; private set; }

        [Inject]
        public void Construct(ApplicationManager applicationManager, GameObject gameObject)
        {
            _applicationManager = applicationManager;
            _gameObject = gameObject;

            //_jumpButton = _gameObject.transform.Find("Panel/JumpButton").GetComponent<ExtraButton>(); //TODO odkomentuj
        }

        public void RegisterOnJumpButtonPointerDown(Action action)
        {
            _jumpButton.onPointerDown += () => action();
        }
        //
        //        public void RegisterOnMoveForwardButtonPointerUp(Action action)
        //        {
        //            _jumpButton.onPointerUp += () => action();
        //        }

        public void Initialize()
        {
            if (_applicationManager.Platform != RuntimePlatform.Android)
            {
                _gameObject.SetActive(false);
            }
        }
    }
}