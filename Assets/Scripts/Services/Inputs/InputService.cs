using UnityEngine;

namespace Pewpew.Services.Inputs
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string FireButton = "Fire";

        public abstract Vector2 Axis { get; }

        public abstract bool IsAttackButtonUp();
    }

    public class StandaloneInputService: InputService
    {
        public override Vector2 Axis
        {
            get 
            { 
                return new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical);
            }
        }

        public override bool IsAttackButtonUp()
        {
            return Input.GetButtonUp(FireButton);
        }
    }
}