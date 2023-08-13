using Pewpew.Infrastructure.Services;
using Pewpew.Services.Inputs;
using Pewpew.Utils;
using UnityEngine;

namespace Pewpew.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField]
        private CharacterController CharacterController;
        [SerializeField]
        private float MovementSpeed;
        [SerializeField]
        private float DeltaTorque;

        private IInputService _inputService;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            if (Mathf.Abs(_inputService.VerticalAxis) > Constants.Epsilon)
                CharacterController.Move(transform.forward * Mathf.Sign(_inputService.VerticalAxis) * MovementSpeed * Time.deltaTime);
        }

        private void Rotate()
        {
            if (Mathf.Abs(_inputService.Torque) > Constants.Epsilon)
                transform.Rotate(new Vector3(0, 1, 0), DeltaTorque * Mathf.Sign(_inputService.Torque));
        }
    }
}
