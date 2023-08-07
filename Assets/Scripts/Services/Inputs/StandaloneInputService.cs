using UnityEngine;

namespace Pewpew.Services.Inputs
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis => UnityAxis();

        private Vector2 UnityAxis()
        {
            return new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
        }

        public override bool IsAttackButtonUp()
        {
            return Input.GetButtonUp(FireButton);
        }
    }
}