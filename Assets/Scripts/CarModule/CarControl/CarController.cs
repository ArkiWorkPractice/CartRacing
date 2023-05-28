using System.Collections.Generic;
using System.Text;
using Models.CarModule;
using ServiceLocatorModule;
using Services.Input;
using TMPro;
using UnityEngine;

namespace CarModule.CarControl
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarController : MonoBehaviour
    {
        // debug
        [SerializeField] private TextMeshProUGUI infoText;

        // car config
        [SerializeField] private List<CarAxle> axles;
        [SerializeField] private Rigidbody carRigidbody;
        [SerializeField] private Transform centerOfMass;

        private CarConfig _config;

        // calculates
        private float _currentMotorTorque;
        private float _currentSpeed;
        private float _currentSteeringAngle;
        private bool _carIsGrounded;
        private CarMovingData _movingData;
        public CarMovingData MovingData => _movingData;
        
        // input
        private float _brakeInput;
        private float _gasInput;
        private float _turnInput;

        // additional services
        private InputService _input;


        public void Initialize(CarConfig config)
        {
            _config = config;
        }

        private void Start()
        {
            _input = ServiceLocator.Instance.GetService<InputService>();

            carRigidbody.centerOfMass = centerOfMass.position;
        }

        private void Update()
        {
            _currentSpeed = carRigidbody.velocity.magnitude;
            CheckInput();
            UpdateWheelMesh();
            SaveMovingData();
            ShowNewInfo();
        }


        private void FixedUpdate()
        {
            ApplyMotorTorque();
            ApplySteering();
            ApplyBrake();
        }

        private void ApplySteering()
        {
            _currentSteeringAngle = _turnInput * _config.SteeringCurve.Evaluate(_currentSpeed);

            foreach (var axle in axles)
            {
                if (axle.canSteer)
                {
                    axle.ApplySteering(_currentSteeringAngle);
                }
            }
        }

        private void ApplyBrake()
        {
            axles[0].ApplyBrakePower(_brakeInput * _config.BrakeForce * 0.7f);
            axles[1].ApplyBrakePower(_brakeInput * _config.BrakeForce * 0.3f);
        }

        private void ApplyMotorTorque()
        {
            foreach (var axle in axles)
            {
                if (axle.hasMotor)
                {
                    _currentMotorTorque = _config.MaxMotorTorque * _gasInput;
                    axle.ApplyMotorTorque(_currentMotorTorque);
                }
            }
        }

        private void UpdateWheelMesh()
        {
            foreach (var axle in axles)
            {
                axle.ApplyLocalPositionToVisuals();
            }
        }

        private void CheckInput()
        {
            _gasInput = _input.GetDirection();
            _turnInput = _input.GetTurn();

            var forward = transform.forward;

            float movingDirection = Vector3.Dot(forward, carRigidbody.velocity);
            if (movingDirection < -0.5f && _gasInput > 0)
            {
                _brakeInput = Mathf.Abs(_gasInput);
            }
            else if (movingDirection > 0.5f && _gasInput < 0)
            {
                _brakeInput = Mathf.Abs(_gasInput);
            }
            else
            {
                _brakeInput = 0;
            }

            if (_gasInput < 0.5 && _gasInput > -0.5)
            {
                _brakeInput = 1;
            }
        }

        private void ShowNewInfo()
        {
            if (infoText)
                return;

            StringBuilder info = new StringBuilder();
            info.Append($"Motor Torque: {_currentMotorTorque}");
            info.Append($"\nRB speed: {_currentSpeed}");
            info.Append($"\nSteering: {_currentSteeringAngle}");
            info.Append($"\nDirection: {_gasInput}");

            infoText.text = info.ToString();
        }

        public void SaveMovingData()
        {
            CheckGrounded();
            
            _movingData = new CarMovingData(transform.position, transform.rotation, _carIsGrounded);
        }
        
        private void CheckGrounded()
        {
            foreach (var axle in axles)
            {
                if (!axle.IsGrounded())
                {
                    _carIsGrounded = false;
                    return;
                }
            }

            _carIsGrounded = true;
        }
        
        public bool CarIsGrounded()
        {
            return _carIsGrounded;
        }

        public void Reset()
        {
            _currentMotorTorque = 0;
            _currentSpeed = 0;
            _currentSteeringAngle = 0;

            // input
            _brakeInput = 0;
            _gasInput = 0;
            _turnInput = 0;

            foreach (var axle in axles)
            {
                axle.ApplySteering(0);
                axle.ApplyMotorTorque(0);
            }

            carRigidbody.velocity = Vector3.zero;
        }
    }
}