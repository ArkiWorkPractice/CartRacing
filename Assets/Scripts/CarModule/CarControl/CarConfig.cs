using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CarModule.CarControl
{
    [CreateAssetMenu(fileName = "CarConfig", menuName = "CarModule/CarConfig", order = 1)]
    public class CarConfig : ScriptableObject
    {
        // car config
        [SerializeField] private List<CarAxle> axles;
        [SerializeField] private float maxMotorTorque;
        [SerializeField] private float maxSteeringAngle;
        [SerializeField] private Transform centerOfMass;
        [SerializeField] private float accelerationTime;
        [SerializeField] private int maxSpeed;
        
        public const float OneStepTimeWaitAtDelayAcceleration = 0.1f;
        public const float HandbrakeForce = Mathf.Infinity;
        public const float ResistanceForce = 1000;
    }
}