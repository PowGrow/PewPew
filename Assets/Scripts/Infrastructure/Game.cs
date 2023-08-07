using Pewpew.Services.Inputs;

namespace Pewpew.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public static IInputService InputService { get; set; }
        public Game()
        {
            StateMachine = new GameStateMachine();
            StateMachine.Enter<BootstrapState>();
        }
    }
}