using System;
using System.Collections;
using System.Collections.Generic;
using Services.Input;
using TMPro;
using UnityEngine;

namespace CarModule
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI speedText;
        
        // car config
        [SerializeField] private List<CarAxle> axles;
        [SerializeField] private float maxMotorTorque = 500;
        [SerializeField] private float maxSteeringAngle = 30;
        [SerializeField] private float centerOfMass = 30;
        
        // calculates
        private float _currentMotorTorque = 500;
        private bool _handbrakeActivated = false;
        
        // additional services
        private InputService _input;
        private Rigidbody _rigidbody;
        private Coroutine _gainSpeedCoroutine;
        private Coroutine _decreaseSpeedCoroutine;

        private void Start()
        {
            _input = new InputService();
            _input.HandbrakeStateChanged += OnHandbrakeSwitched;
            _input.MoveStateChanged += OnMoveStateChanged;
            

            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.centerOfMass = new Vector3(0, centerOfMass, 0);
        }
        

        private void OnHandbrakeSwitched(bool handbrakeActive)
        {
            _handbrakeActivated = handbrakeActive;
        }

        private void OnMoveStateChanged(bool isMoving)
        {
            if (isMoving)
            {
                if (_decreaseSpeedCoroutine != null) StopCoroutine(_decreaseSpeedCoroutine);
                _gainSpeedCoroutine = StartCoroutine(GainSpeed());
            }
            else
            {
                if (_gainSpeedCoroutine != null) StopCoroutine(_gainSpeedCoroutine);
                _decreaseSpeedCoroutine = StartCoroutine(DecreaseSpeed());
            }
                
        }

        

        private void Update()
        {
            ShowSpeed();
        }

        private void FixedUpdate()
        {
            MoveWheelColliders();
        }

        private void MoveWheelColliders()
        {
            float motor = _currentMotorTorque * _input.GetDirection().y;
            float steering = maxSteeringAngle * _input.GetDirection().x;
            
            foreach (var axleInfo in axles) {
                if (axleInfo.canSteer) {
                    axleInfo.leftWheelCollider.steerAngle = steering;
                    axleInfo.rightWheelCollider.steerAngle = steering;
                }
                if (axleInfo.hasMotor ) {
                    axleInfo.leftWheelCollider.motorTorque = motor;
                    axleInfo.rightWheelCollider.motorTorque = motor;
                }

                if (!axleInfo.hasHandbrake && _handbrakeActivated)
                {
                    axleInfo.leftWheelCollider.brakeTorque = 1000;
                    axleInfo.rightWheelCollider.brakeTorque = 1000;
                }
                else if (axleInfo.hasHandbrake && _handbrakeActivated)
                {
                    axleInfo.leftWheelCollider.brakeTorque = Mathf.Infinity;
                    axleInfo.rightWheelCollider.brakeTorque = Mathf.Infinity;
                }
                else if (axleInfo.hasHandbrake && !_handbrakeActivated)
                {
                    axleInfo.leftWheelCollider.brakeTorque = 0;
                    axleInfo.rightWheelCollider.brakeTorque = 0;
                }
                
                
                ApplyLocalPositionToVisuals(axleInfo.leftWheelTransform, axleInfo.leftWheelCollider);
                ApplyLocalPositionToVisuals(axleInfo.rightWheelTransform, axleInfo.rightWheelCollider);
            }
        }
        
        private void ApplyLocalPositionToVisuals(Transform wheelTransform, WheelCollider wheelCollider)
        {
            wheelCollider.GetWorldPose(out var position, out var rotation);

            wheelTransform.transform.position = position;
            wheelTransform.transform.rotation = rotation;
        }


        private void ShowSpeed()
        {
            speedText.text = ((2 * Mathf.PI * axles[0].leftWheelCollider.radius * axles[0].leftWheelCollider.rpm * 60) / 1000).ToString();
        }
        
        private IEnumerator DecreaseSpeed()
        {
            foreach (var axle in axles)
            {
                axle.leftWheelCollider.brakeTorque = 1000;
                axle.rightWheelCollider.brakeTorque = 1000;
            }

            while (_currentMotorTorque > 0)
            {
                yield return new WaitForSeconds(0.3f);

                _currentMotorTorque -= 100;
                Debug.Log(_currentMotorTorque);
            }
        }
        
        private IEnumerator GainSpeed()
        {
            foreach (var axle in axles)
            {
                axle.leftWheelCollider.brakeTorque = 0;
                axle.rightWheelCollider.brakeTorque = 0;
            }
            
            while (_currentMotorTorque < maxMotorTorque)
            {
                yield return new WaitForSeconds(0.3f);

                _currentMotorTorque += 100;
                
                Debug.Log(_currentMotorTorque);
            }
        }
    }
}