using System;
using UnityEngine;

namespace CarModule
{
    [Serializable]
    public class CarAxle
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool hasMotor;
        public bool canSteer;
    }
}