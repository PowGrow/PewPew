using Pewpew.Infrastructure;
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

        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Start()
        {
            _camera = Camera.main;
            CameraFollow();
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;
            if(_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector; // Face to movement vector
            }


            CharacterController.Move(MovementSpeed * movementVector * Time.deltaTime);
        }
        private void CameraFollow() => 
            _camera.GetComponent<CameraFollow>().Follow(gameObject);
    }
}
