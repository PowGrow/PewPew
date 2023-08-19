using Pewpew.Infrastructure.Services;
using Pewpew.Services.Inputs;
using Pewpew.Utils;
using UnityEngine;

namespace Pewpew.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody ShipRigidbody;
        [SerializeField]
        private Transform ShipTransform;
        [SerializeField]
        private float Speed;
        [SerializeField]
        private float DeltaTorque;

        private IInputService _inputService;
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void FixedUpdate()
        {
            Move(ShipRigidbody);
            Rotate(ShipTransform);
        }

        private void Move(Rigidbody shipRigidbody)
        {
            if (Mathf.Abs(_inputService.VerticalAxis) > Constants.Epsilon || Mathf.Abs(_inputService.Torque) > Constants.Epsilon)
            {
                var movementVector = new Vector3(_inputService.Torque,0, _inputService.VerticalAxis);
                movementVector *= shipRigidbody.mass * Speed * Time.deltaTime;
                shipRigidbody.AddForce(movementVector);
            }
        }

        private void Rotate(Transform shipTransform)
        {
            var lookAtRotation = LookRotation(shipTransform.transform);
            if (Quaternion.Angle(shipTransform.rotation, lookAtRotation) > Constants.Epsilon)
                shipTransform.rotation = Quaternion.RotateTowards(shipTransform.rotation, lookAtRotation, DeltaTorque * Time.deltaTime);
        }

        private Quaternion LookRotation(Transform shipTransform)
        {
            Vector3 lookAtPosition = LookPosition(shipTransform);
            var lookAtDirection = (lookAtPosition - shipTransform.position).normalized;
            return Quaternion.LookRotation(lookAtDirection);
        }

        private Vector3 LookPosition(Transform shipTransform)
        {
            var mousePositionRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit mousePositionRayCastHitInfo;
            Physics.Raycast(mousePositionRay, out mousePositionRayCastHitInfo);
            return new Vector3(mousePositionRayCastHitInfo.point.x, shipTransform.position.y, mousePositionRayCastHitInfo.point.z);
        }
    }
}
