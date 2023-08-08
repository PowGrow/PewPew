namespace Pewpew.Infrastructure.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}