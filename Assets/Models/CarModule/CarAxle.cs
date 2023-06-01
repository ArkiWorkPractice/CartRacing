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
        [SerializeField] private float brakeReducer;

        [SerializeField] private bool hasMotor;
        [SerializeField] private bool canSteer;
        [SerializeField] private bool hasHandbrake;
        
        public bool HasMotor => hasMotor;
        public bool CanSteer => canSteer;
        public bool HasHandbrake => hasHandbrake;

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

        public void ApplyBrakePower(float configHandbrakeForce, bool isHandbrake = false)
        {
            float brakeMultiplier = isHandbrake ? 1 : brakeReducer;
            leftWheelCollider.brakeTorque = configHandbrakeForce * brakeMultiplier;
            rightWheelCollider.brakeTorque = configHandbrakeForce * brakeMultiplier;
        }

        public bool IsGrounded()
        {
            return leftWheelCollider.isGrounded && rightWheelCollider.isGrounded;
        }
    }
}