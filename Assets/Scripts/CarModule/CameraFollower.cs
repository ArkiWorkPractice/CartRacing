using System;
using UnityEngine;

namespace CarModule
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float turnSpeed;
        
        [SerializeField] private bool freeRotation;
        [SerializeField] private float rotationSensitivity = 3.5f;
        private const float YMinRotate = -20; // Min vertical angle
        private const float YMaxRotate = 80;

        [SerializeField] private float currentDistance;
        [SerializeField] private float minDistance;
        [SerializeField] private float maxDistance;

        [SerializeField] private Vector3 offset = new Vector3(0, 1.5f, 0.5f);

        private Vector3 _currentCameraTransform;

        private void Start()
        {
            _currentCameraTransform = transform.position;

            
        }

        public void LateUpdate()
        {
            if (freeRotation)
            {
                
            }
            else
            {
                
            }
            
            UpdatePosition();
            UpdateRotation();
        }

        private void UpdatePosition()
        {
            _currentCameraTransform = target.position;
            
            Vector3 t = _currentCameraTransform + transform.rotation * offset;
            Vector3 f = transform.rotation * -Vector3.forward;
            
            transform.position = t + f * currentDistance;
        }

        private void UpdateRotation()
        {
            var xRotation = Mathf.Clamp(target.rotation.x, YMinRotate, YMaxRotate);


            transform.rotation = new Quaternion(xRotation, target.rotation.y, target.rotation.z, target.rotation.w);
        }
    }
}