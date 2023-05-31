using System;
using UnityEngine;

namespace Models.CarModule
{
    [Serializable]
    public class CarConfig
    {

        [SerializeField] private float maxMotorTorque;
        [SerializeField] private AnimationCurve steeringCurve;
        [SerializeField] private Vector2 steeringCurveStart;
        [SerializeField] private Vector2 steeringCurveEnd;
        [SerializeField] private float steeringAngle;
        [SerializeField] private float brakeForce;
        
        [SerializeField] private int delayBetweenSaving;
        [SerializeField] private int immortalTimeInMilliseconds;
        [SerializeField] private int maxHealth;
        [SerializeField] private float directionSmoothing;
        [SerializeField] private float turnSmoothing;
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;

            public CarConfig(CarConfig carConfig)
        {   
            maxMotorTorque = carConfig.maxMotorTorque;
            brakeForce = carConfig.brakeForce;
       
            immortalTimeInMilliseconds = carConfig.immortalTimeInMilliseconds;
            maxHealth = carConfig.maxHealth;
            steeringCurve = carConfig.steeringCurve;
            steeringAngle = carConfig.steeringAngle;
            delayBetweenSaving = carConfig.delayBetweenSaving;
            directionSmoothing = carConfig.directionSmoothing;
            turnSmoothing = carConfig.turnSmoothing;

            minSpeed = carConfig.minSpeed;
            maxSpeed = carConfig.maxSpeed;

            steeringCurveStart = carConfig.steeringCurveStart;
            steeringCurveEnd = carConfig.steeringCurveEnd;
        }   

        
        public float MaxMotorTorque => maxMotorTorque;
        public float BrakeForce => brakeForce;
        public int MaxHealth => maxHealth;
        public int ImmortalTimeInMilliseconds => immortalTimeInMilliseconds;
        public AnimationCurve SteeringCurve => steeringCurve;
        public float SteeringAngle => steeringAngle;
        public int DelayBetweenSaving => delayBetweenSaving;
        public float MinSpeed => minSpeed;

        public float MaxSpeed => maxSpeed;
        public Vector2 SteeringCurveStart => steeringCurveStart;

        public Vector2 SteeringCurveEnd => steeringCurveEnd;
        

        public CarConfig Copy()
        {
            return new CarConfig(this);
        }
    }
}
