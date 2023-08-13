using Pewpew.Infrastructure.Services;
using UnityEngine;

namespace Pewpew.Services.Inputs
{
    public interface IInputService: IService
    {
        public abstract float VerticalAxis { get; }

        public abstract float Torque { get; }

        public abstract bool IsAttackButtonUp();
    }
}