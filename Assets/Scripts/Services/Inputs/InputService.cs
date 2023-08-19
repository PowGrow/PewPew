using UnityEngine;

namespace Pewpew.Services.Inputs
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string FireButton = "Fire1";

        public abstract float zAxis { get; }

        public abstract float xAxis { get; }

        public abstract bool IsAttackButtonDown();
    }
}