using UnityEngine;

namespace Pewpew.Services.Inputs
{
    public interface IInputService
    {
        public abstract Vector2 Axis { get; }

        public abstract bool IsAttackButtonUp();
    }
}