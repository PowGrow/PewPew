using UnityEngine;

namespace Pewpew.Services.Inputs
{
    public class StandaloneInputService : InputService
    {
        public override float VerticalAxis => VertAxis();

        public override float Torque => TorqueAxis();

        private float VertAxis()
        {
            return Input.GetAxis(Vertical);
        }

        private float TorqueAxis()
        {
            return Input.GetAxis(Horizontal);
        }

        public override bool IsAttackButtonUp()
        {
            return Input.GetButtonUp(FireButton);
        }
    }
}