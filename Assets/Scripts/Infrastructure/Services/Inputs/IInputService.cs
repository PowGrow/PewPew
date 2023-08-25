using Pewpew.Infrastructure.Services;

namespace Pewpew.Services.Inputs
{
    public interface IInputService: IService
    {
        public abstract float zAxis { get; }

        public abstract float xAxis { get; }

        public abstract bool IsAttackButtonDown();
    }
}