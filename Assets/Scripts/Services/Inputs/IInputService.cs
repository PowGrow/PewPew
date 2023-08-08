using Pewpew.Infrastructure.Services;
using UnityEngine;

namespace Pewpew.Services.Inputs
{
    public interface IInputService: IService
    {
        public abstract Vector2 Axis { get; }

        public abstract bool IsAttackButtonUp();
    }
}