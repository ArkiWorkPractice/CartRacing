using System;
using UnityEngine;

namespace CarModule
{
    [Serializable]
    public class CarAxle
    {
        public WheelCollider leftWheelCollider;
        public WheelCollider rightWheelCollider;
        public bool hasMotor;
        public bool canSteer;
        public Transform leftWheelTransform;
        public Transform rightWheelTransform;
        public bool hasHandbrake;
    }
}