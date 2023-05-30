using UnityEngine;

namespace CarModule.CarControl
{
    public class CarCameraFollower 
    {
        private readonly Transform _cameraTransform;
        private readonly Transform _target;
        private readonly Rigidbody _targetRigidbody;

        private readonly Vector3 _offset;
        private readonly int _turnAngleMultiplier;
        private readonly int _cameraSpeed;
        
        public CarCameraFollower(Transform cameraTransform, Transform target, Rigidbody targetRigidbody, Vector3 offset, int turnAngleMultiplier, int cameraSpeed)
        {
            _cameraTransform = cameraTransform;
            _target = target;
            _targetRigidbody = targetRigidbody;
            _offset = offset;
            _turnAngleMultiplier = turnAngleMultiplier;
            _cameraSpeed = cameraSpeed;
        }

        public void OnLateUpdate()
        {
            /*
            Vector3 targetForward = (_targetRigidbody.velocity + _target.transform.forward).normalized;

            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position,
                _target.position + _target.TransformVector(_offset) + targetForward * (-1f * _turnAngleMultiplier),
                _cameraSpeed * Time.deltaTime);
                */
            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position,  _target.position + _offset, Time.deltaTime * _cameraSpeed);

            _cameraTransform.LookAt(_target);
        }
    }
}