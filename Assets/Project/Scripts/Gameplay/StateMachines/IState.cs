namespace Gameplay.StateMachines
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Execute();
    }
}
