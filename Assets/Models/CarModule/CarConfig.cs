using System;
using UnityEngine;

namespace Models.CarModule
{
    [Serializable]
    public class CarConfig
    {

        [SerializeField] private float maxMotorTorque;
        [SerializeField] private AnimationCurve steeringCurve;
        [SerializeField] private float brakeForce;
        
        [SerializeField] private int delayBetweenSaving;
        [SerializeField] private int immortalTimeInMilliseconds;
        [SerializeField] private int maxHealth;
        [SerializeField] private float directionSmoothing;
        [SerializeField] private float turnSmoothing;
        
        public CarConfig(CarConfig carConfig)
        {   
            maxMotorTorque = carConfig.maxMotorTorque;
            brakeForce = carConfig.brakeForce;
       
            immortalTimeInMilliseconds = carConfig.immortalTimeInMilliseconds;
            maxHealth = carConfig.maxHealth;
            steeringCurve = carConfig.steeringCurve;
            delayBetweenSaving = carConfig.delayBetweenSaving;
            directionSmoothing = carConfig.directionSmoothing;
            turnSmoothing = carConfig.turnSmoothing;
        }   

        
        public float MaxMotorTorque => maxMotorTorque;
        public float BrakeForce => brakeForce;
        public int MaxHealth => maxHealth;
        public int ImmortalTimeInMilliseconds => immortalTimeInMilliseconds;
        public AnimationCurve SteeringCurve => steeringCurve;
        public int DelayBetweenSaving => delayBetweenSaving;
        public float DirectionSmoothing => directionSmoothing;

        public float TurnSmoothing => turnSmoothing;

        public CarConfig Copy()
        {
            return new CarConfig(this);
        }
    }
}
