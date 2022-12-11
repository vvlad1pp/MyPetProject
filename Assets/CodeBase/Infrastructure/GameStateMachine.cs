using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure
{
    public class GameStateMachine
    {
        public readonly Dictionary<Type, IState> states;
        private IState activeState;

        public GameStateMachine()
        {
            states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this),
            };
        }

        public void Enter<TState>() where TState : IState
        {
            activeState?.Exit();
            IState state = states[typeof(TState)];
            activeState = state;
            state.Enter();
        } 
    }
}