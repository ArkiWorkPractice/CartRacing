using System;
using UnityEngine;

namespace CarModule
{
    [Serializable]
    public class CarAxle
    {
        public WheelCollider leftWheelCollider;
        public WheelCollider rightWheelCollider;
        public Transform leftWheelTransform;
        public Transform rightWheelTransform;
        
        public bool hasMotor;
        public bool canSteer;
        public bool hasHandbrake;
        
        public void ApplyLocalPositionToVisuals()
        {
            leftWheelCollider.GetWorldPose(out var position, out var rotation);
            leftWheelTransform.position = position;
            leftWheelTransform.rotation = rotation;
            
            rightWheelCollider.GetWorldPose(out position, out rotation);
            rightWheelTransform.position = position;
            rightWheelTransform.rotation = rotation;
        }
    }
}