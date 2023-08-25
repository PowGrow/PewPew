using Pewpew.Infrastructure.Services;
using Pewpew.Services.Inputs;
using Pewpew.Utils;
using UnityEngine;

namespace Pewpew.Player
{
    public class Movement
    { 
        private IInputService _inputService;
        private Camera _mainCamera;

        private Stats _stats;
        private Rigidbody _shipRigidbody;
        private Transform _shipTransform;
        private float _deltaTorque;
        public Movement(Stats stats, Rigidbody shipRigidbody, Transform shipTransform, float deltaTorque)
        {
            _stats = stats;
            _shipRigidbody = shipRigidbody;
            _shipTransform = shipTransform;
            _deltaTorque = deltaTorque;
            _mainCamera = Camera.main;
            _inputService = AllServices.Container.Single<IInputService>();
        }

        public void Execute()
        {
            Move(_shipRigidbody);
            Rotate(_shipTransform);
        }

        private void Move(Rigidbody shipRigidbody)
        {
            if (Mathf.Abs(_inputService.zAxis) > Constants.Epsilon || Mathf.Abs(_inputService.xAxis) > Constants.Epsilon)
            {
                var movementVector = new Vector3(_inputService.xAxis,0, _inputService.zAxis);
                movementVector.Normalize();
                movementVector *= shipRigidbody.mass * _stats.Speed * Time.deltaTime;
                shipRigidbody.AddForce(movementVector);
            }
        }

        private void Rotate(Transform shipTransform)
        {
            var lookAtRotation = LookRotation(shipTransform.transform);
            if (Quaternion.Angle(shipTransform.rotation, lookAtRotation) > Constants.Epsilon)
                shipTransform.rotation = Quaternion.RotateTowards(shipTransform.rotation, lookAtRotation, _deltaTorque * Time.deltaTime);
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
