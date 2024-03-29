﻿using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.States.SaveLoad;
using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.ServiceLocator;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private const string Payload = "Main";
        private IState _stateImplementation;
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }
        public void Enter() => 
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        
        public void Exit()
        {
        }
        
        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadProgressState>();
        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IAsset>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAsset>()));
            _services.RegisterSingle<ISavaLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(),
                _services.Single<IGameFactory>()));
        }
        private static IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}