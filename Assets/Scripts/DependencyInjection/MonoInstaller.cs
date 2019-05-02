using Assets.Scripts.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Di
{
    public class MonoInstaller : MonoInstaller<MonoInstaller>
    {
        public override void InstallBindings()
        {
            var cameraGo = GameObject.Find("Player/Camera");
            var playerGo = GameObject.Find("Player");
            var playerMeshGo = GameObject.Find("Player/Sci-Fi_Soldier");

            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle().NonLazy();
            Container.Bind<ApplicationManager>().AsSingle().NonLazy();
            Container.BindFromImplementedInterface<IInput>();
            Container.Bind<ITickable>().To<InputListener>().AsSingle();

            //            Container.Bind<GameObject>().FromMethod(c => GameObject.Find("Player/Camera")).WhenInjectedInto<CameraController>();
            //            Container.Bind<GameObject>().FromMethod(c => GameObject.Find("Player/Capsule")).WhenInjectedInto<CameraController>();
            Container.BindInterfacesAndSelfTo<CameraController>().AsSingle().WithArguments(cameraGo, playerMeshGo);

            Container.Bind<GameObject>().FromMethod(c => GameObject.Find("AndroidInputCanvas")).WhenInjectedInto<AndroidInputCanvasController>();
            Container.BindInterfacesAndSelfTo<AndroidInputCanvasController>().AsSingle();

            //            Container.Bind<GameObject>().FromMethod(c => GameObject.Find("Player")).WhenInjectedInto<PlayerController>();
            //            Container.Bind<GameObject>().FromMethod(c => GameObject.Find("Player/Capsule")).WhenInjectedInto<PlayerController>();
            Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().WithArguments(playerGo, playerMeshGo);
        }
    }
}