using UnityEngine;

namespace CarModule.CarControl
{
    public class CarCameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Rigidbody targetRigidbody;

        [SerializeField] private Vector3 offset;
        [SerializeField] private int turnAngleMultiplier;
        [SerializeField] private int cameraSpeed;
        
        private void LateUpdate()
        {
            Vector3 targetForward = (targetRigidbody.velocity + target.transform.forward).normalized;
            
            transform.position = Vector3.Lerp(transform.position, 
                target.position + target.TransformVector(offset) + targetForward * (-1f * turnAngleMultiplier), 
                cameraSpeed * Time.deltaTime);
            
            transform.LookAt(target);
        }
    }
}