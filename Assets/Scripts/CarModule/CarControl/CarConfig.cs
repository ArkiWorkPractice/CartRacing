using System;
using UnityEngine;

namespace CarModule.CarControl
{
    [Serializable]
    public class CarConfig
    {
        [SerializeField] private float maxMotorTorque;
        [SerializeField] private float maxSteeringAngle;
        [SerializeField] private float handbrakeForce;

        [SerializeField] private int accelerationTimeInMilliseconds;
        [SerializeField] private int maxSpeed;

        [SerializeField] private int oneStepTimeWaitAtDelayAccelerationInMilliseconds; // 0.1f
        [SerializeField] private float resistanceForce; // 1000
        [SerializeField] private int immortalTimeInMilliseconds;
        [SerializeField] private int maxHealth;
        
        public CarConfig(float maxMotorTorque, float handbrakeForce, float maxSteeringAngle, int accelerationTimeInMilliseconds, int maxSpeed,
            int oneStepTimeWaitAtDelayAccelerationInMilliseconds, float resistanceForce, int immortalTimeInMilliseconds, int maxHealth)
        {
            this.maxMotorTorque = maxMotorTorque;
            this.maxSteeringAngle = maxSteeringAngle;
            this.handbrakeForce = handbrakeForce;
            this.accelerationTimeInMilliseconds = accelerationTimeInMilliseconds;
            this.maxSpeed = maxSpeed;
            this.oneStepTimeWaitAtDelayAccelerationInMilliseconds = oneStepTimeWaitAtDelayAccelerationInMilliseconds;
            this.resistanceForce = resistanceForce;
            this.immortalTimeInMilliseconds = immortalTimeInMilliseconds;
            this.maxHealth = maxHealth;
        }

        public CarConfig(CarConfig carConfig)
        {   
            maxMotorTorque = carConfig.maxMotorTorque;
            maxSteeringAngle = carConfig.maxSteeringAngle;
            handbrakeForce = carConfig.handbrakeForce;
            accelerationTimeInMilliseconds = carConfig.accelerationTimeInMilliseconds;
            maxSpeed = carConfig.maxSpeed;
            oneStepTimeWaitAtDelayAccelerationInMilliseconds = carConfig.oneStepTimeWaitAtDelayAccelerationInMilliseconds;
            resistanceForce = carConfig.resistanceForce;
            immortalTimeInMilliseconds = carConfig.immortalTimeInMilliseconds;
            maxHealth = carConfig.maxHealth;
        }   

        
        public float MaxMotorTorque => maxMotorTorque;
        public float MaxSteeringAngle => maxSteeringAngle;
        public float HandbrakeForce => handbrakeForce;
        public int AccelerationTimeInMilliseconds => accelerationTimeInMilliseconds;
        public int MaxSpeed => maxSpeed;
        public int OneStepTimeWaitAtDelayAccelerationInMilliseconds => oneStepTimeWaitAtDelayAccelerationInMilliseconds;
        public float ResistanceForce => resistanceForce;
        public int MaxHealth => maxHealth;
        public int ImmortalTimeInMilliseconds => immortalTimeInMilliseconds;

        public CarConfig Copy()
        {
            return new CarConfig(this);
        }
    }
}