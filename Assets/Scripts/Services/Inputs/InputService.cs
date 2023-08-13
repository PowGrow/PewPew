using UnityEngine;

namespace Pewpew.Services.Inputs
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string FireButton = "Fire";

        public abstract float VerticalAxis { get; }

        public abstract float Torque { get; }

        public abstract bool IsAttackButtonUp();
    }
}