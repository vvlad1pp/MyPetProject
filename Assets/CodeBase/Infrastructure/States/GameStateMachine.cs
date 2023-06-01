﻿using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.States.SaveLoad;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.ServiceLocator;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain,
                    services.Single<IGameFactory>(),
                    services.Single<IPersistentProgressService>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(),
                    services.Single<ISavaLoadService>()
                ),
                [typeof(GameLoopState)] = new GameLoopState(this)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}