using System;
using System.Collections.Generic;
using Services.Input;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace CarModule
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private List<CarAxle> axles;
        [SerializeField] private float motorTorque;
        [SerializeField] private float steeringAngle;
        
        private InputService _input;

        private void Start()
        {
            _input = new InputService();
        }

        private void FixedUpdate()
        {
            float motor = motorTorque * _input.GetDirection().X;
            float steering = steeringAngle * _input.GetDirection().Y;
            
            foreach (CarAxle axleInfo in axles) {
                if (axleInfo.canSteer) {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }
                if (axleInfo.hasMotor) {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                }
            }
        }
    }
}