using UnityEngine;

namespace CarModule.CarControl
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Vector3 offset;
        [SerializeField] int cameraSmoothing;

        private Transform _target;

        private void FixedUpdate()
        {
            if (!_target)
                return;

            MoveCamera();
        }

        private void MoveCamera()
        {
            transform.position = Vector3.Lerp(transform.position,  
                _target.position + _target.TransformVector(offset) + _target.forward
                , Time.deltaTime * cameraSmoothing);

            transform.LookAt(_target);
        }

        public void Follow(Transform target)
        {
            _target = target;
        }
    }
}