namespace Pewpew.Infrastructure
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}