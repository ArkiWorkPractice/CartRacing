using System;
using UnityEngine;

namespace Models.CarModule
{
    [Serializable]
    public class CarAxle
    {
        [SerializeField] private WheelCollider leftWheelCollider;
        [SerializeField] private WheelCollider rightWheelCollider;
        [SerializeField] private Transform leftWheelTransform;
        [SerializeField] private Transform rightWheelTransform;
        
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

        public void ApplyMotorTorque(float torque)
        {
            leftWheelCollider.motorTorque = torque;
            rightWheelCollider.motorTorque = torque;
        }

        public void ApplySteering(float currentSteeringAngle)
        {
            leftWheelCollider.steerAngle = currentSteeringAngle;
            rightWheelCollider.steerAngle = currentSteeringAngle;
        }

        public void ApplyBrakePower(float configHandbrakeForce)
        {
            leftWheelCollider.brakeTorque = configHandbrakeForce;
            rightWheelCollider.brakeTorque = configHandbrakeForce;
        }

        public bool IsGrounded()
        {
            return leftWheelCollider.isGrounded && rightWheelCollider.isGrounded;
        }
    }
}