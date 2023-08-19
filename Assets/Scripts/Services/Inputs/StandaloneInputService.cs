using UnityEngine;

namespace Pewpew.Services.Inputs
{
    public class StandaloneInputService : InputService
    {
        public override float zAxis => VerticalAxis();

        public override float xAxis => HorizontalAxis();

        private float VerticalAxis()
        {
            return Input.GetAxis(Vertical);
        }

        private float HorizontalAxis()
        {
            return Input.GetAxis(Horizontal);
        }

        public override bool IsAttackButtonDown()
        {
            return Input.GetButton(FireButton);
        }
    }
}