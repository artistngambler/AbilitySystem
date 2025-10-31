using System;
using System.Collections.Generic;
using System.Linq;

namespace Gameplay.StateMachines.GameSM
{
    public class GameStateMachine
    {
        readonly List<IState> states;

        IState currentState;

        public GameStateMachine(List<IState> states)
        {
            this.states = states;
        }

        public void SwitchState<T>()
            where T : IState
        {
            if (currentState is T)
            {
                throw new Exception(
                    $"<GameStateMachine::SwitchState>: Can't switch to state {typeof(T)} because it is already current"
                );
            }

            IState newState = states.FirstOrDefault(s => s is T);

            if (newState == null)
            {
                throw new Exception($"<GameStateMachine::SwitchState>: State {typeof(T)} does not exist");
            }

            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }

        public void Execute()
        {
            currentState?.Execute();
        }

        public void Cleanup()
        {
            currentState?.Exit();
            currentState = null;
        }
    }
}
