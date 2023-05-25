using System;
using System.Collections;
using System.Collections.Generic;
using ServiceLocatorModule;
using Services.Input;
using TMPro;
using UnityEngine;

namespace CarModule.CarControl
{
    [RequireComponent(typeof(Rigidbody))]

    public class CarController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI speedText;
        
        // car config
        [SerializeField] private List<CarAxle> axles;
        [SerializeField] private float maxMotorTorque = 500;
        [SerializeField] private float maxSteeringAngle = 30;
        [SerializeField] private Transform centerOfMass;
        [SerializeField] private float accelerationTime;
        [SerializeField] private int maxSpeed;
        
        private const float OneStepTimeWaitAtDelayAcceleration = 0.1f;
        private const float HandbrakeForce = Mathf.Infinity;
        private const float ResistanceForce = 1000;
        
        // calculates
        private float _currentMotorTorque;
        private bool _handbrakeActivated;
        private int _stepAtChangingSpeed;
        
        // additional services
        private InputService _input;
        [SerializeField] private Rigidbody carRigidbody;
        private Coroutine _gainSpeedCoroutine;
        private Coroutine _decreaseSpeedCoroutine;
        private int _carSpeed;

        private void Start()
        {
            _input = ServiceLocator.Instance.GetService<InputService>();
            _input.HandbrakeStateChanged += OnHandbrakeSwitched;
            _input.DirectionChanged += OnDirectionChanged;
            
            carRigidbody = GetComponent<Rigidbody>();
            carRigidbody.centerOfMass = centerOfMass.position;

            _stepAtChangingSpeed =
                Convert.ToInt32(maxMotorTorque / (accelerationTime / OneStepTimeWaitAtDelayAcceleration));
        }

        private void OnHandbrakeSwitched(bool handbrakeActive)
        {
            _handbrakeActivated = handbrakeActive;
        }

        private void OnDirectionChanged(bool isMoving)
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
            foreach (var axle in axles)
            {
               axle.ApplyLocalPositionToVisuals();
            }
            ShowInfo();
        }

        private void FixedUpdate()
        {
            MoveWheelColliders();
        }

        private void MoveWheelColliders()
        {
            foreach (var axle in axles)
            {
                Steer(axle);
                StartMotor(axle);
                ActivateHandbrake(axle);
            }
        }

        private void Steer(CarAxle axle)
        {
            if (axle.canSteer) {
                float steering = maxSteeringAngle * _input.GetTurn();
                   
                axle.leftWheelCollider.steerAngle = steering;
                axle.rightWheelCollider.steerAngle = steering;
            }
        }

        private void StartMotor(CarAxle axle)
        {
            if (axle.hasMotor ) {
                if (carRigidbody.velocity.magnitude > maxSpeed || axles[0].leftWheelCollider.rpm > 7000)
                {
                    axle.leftWheelCollider.motorTorque = 0;
                    axle.rightWheelCollider.motorTorque = 0;
                }
                else
                {
                    float motor = _currentMotorTorque * _input.GetDirection();

                    axle.leftWheelCollider.motorTorque = motor;
                    axle.rightWheelCollider.motorTorque = motor;
                }
            }
        }

        private void ActivateHandbrake(CarAxle axle)
        {
            if (!axle.hasHandbrake && _handbrakeActivated)
            {
                axle.leftWheelCollider.brakeTorque = ResistanceForce;
                axle.rightWheelCollider.brakeTorque = ResistanceForce;
            }
            else if (axle.hasHandbrake && _handbrakeActivated)
            {
                axle.leftWheelCollider.brakeTorque = HandbrakeForce;
                axle.rightWheelCollider.brakeTorque = HandbrakeForce;
            }
            else if (axle.hasHandbrake && !_handbrakeActivated)
            {
                axle.leftWheelCollider.brakeTorque = 0;
                axle.rightWheelCollider.brakeTorque = 0;
            }
        }
        
        private void ApplyLocalPositionToVisuals(Transform wheelTransform, WheelCollider wheelCollider)
        {
            wheelCollider.GetWorldPose(out var position, out var rotation);

            wheelTransform.position = position;
            wheelTransform.rotation = rotation;
        }


        private void ShowInfo()
        {
            _carSpeed = Convert.ToInt32(Math.Abs((2 * Mathf.PI * axles[0].leftWheelCollider.radius * axles[0].leftWheelCollider.rpm * 60) / 1000));
            speedText.text = $"Speed: {_carSpeed}";
            speedText.text += $"\nTrue Speed: {Convert.ToInt32(carRigidbody.velocity.magnitude)}";
            speedText.text += $"\nrpm: {axles[0].leftWheelCollider.rpm}";
        }

        private void AddResistanceForce()
        {
            foreach (var axle in axles)
            {
                axle.leftWheelCollider.brakeTorque = ResistanceForce;
                axle.rightWheelCollider.brakeTorque = ResistanceForce;
            }
        }

        private void StopBrake()
        {
            foreach (var axle in axles)
            {
                axle.leftWheelCollider.brakeTorque = 0;
                axle.rightWheelCollider.brakeTorque = 0;
            }
        }
        
        private IEnumerator DecreaseSpeed()
        {
            AddResistanceForce();

            while (_currentMotorTorque > 0)
            {
                yield return new WaitForSeconds(OneStepTimeWaitAtDelayAcceleration);

                _currentMotorTorque -= _stepAtChangingSpeed;
            }
        }
        
        private IEnumerator GainSpeed()
        {
            StopBrake();
            
            while (_currentMotorTorque < maxMotorTorque)
            {
                yield return new WaitForSeconds(OneStepTimeWaitAtDelayAcceleration);

                _currentMotorTorque += _stepAtChangingSpeed;
            }
        }
    }
}