namespace Pewpew.Infrastructure
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}