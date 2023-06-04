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
        private AnimationCurve _steeringLimitCurve;
        private CarMovingData _movingData;
        public CarMovingData MovingData => _movingData;

        // input
        private Vector2 _currentDirectionInput;
        private bool _handbrake;
        private float _brakeInput;
        private float _gasInput;
        private float _turnInput;

        // additional services
        private InputService _input;

        private bool _canMove;

        public void Initialize(CarConfig config)
        {
            _input = ServiceLocator.Instance.GetService<InputService>();
            _config = config;
            _canMove = true;
            _steeringLimitCurve = new AnimationCurve(new Keyframe(_config.SteeringCurveStart.x, _config.SteeringCurveStart.y), 
                                                              new Keyframe(_config.SteeringCurveEnd.x, _config.SteeringCurveEnd.y));
            _movingData = new CarMovingData(false);
        }

        private void Start()
        {
            carRigidbody.centerOfMass = centerOfMass.localPosition;
        }

        private void Update()
        {
            if (!_canMove)
            {
                return;
            }
            _currentSpeed = carRigidbody.velocity.magnitude;
            CheckInput();
            UpdateWheelMesh();
            SaveMovingData();
            ApplySteering();
            ApplyMotorTorque();
            ApplyBrake();
            ShowNewInfo();
        }

        private void ApplySteering()
        {
            var time = Mathf.InverseLerp(_config.MinSpeed, _config.MaxSpeed, _currentSpeed);
            _currentSteeringAngle = _turnInput * _config.SteeringAngle * _steeringLimitCurve.Evaluate(time);

            foreach (var axle in axles)
            {
                if (axle.CanSteer)
                {
                    axle.ApplySteering(_currentSteeringAngle);
                }
            }
        }

        private void ApplyBrake()
        {
            foreach (var axle in axles)
            {
                if (_handbrake && axle.HasHandbrake)
                {
                    axle.ApplyBrakePower(_config.BrakeForce, true);
                }
                else
                {
                    axle.ApplyBrakePower(_brakeInput * _config.BrakeForce);
                }
            }
        }

        private void ApplyMotorTorque()
        {
            foreach (var axle in axles)
            {
                if (axle.HasMotor)
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
            _handbrake = _input.GetHandbrakeStatus();
            
            _gasInput = _input.Direction.x;

            _turnInput = _input.Direction.y;

            float movingDirection = Vector3.Dot(transform.forward, carRigidbody.velocity);
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
        }

        private void ShowNewInfo()
        {
            if (!infoText)
                return;

            StringBuilder info = new StringBuilder();
            info.Append($"Motor Torque: {_currentMotorTorque}");
            info.Append($"\nRB speed: {_currentSpeed}");
            info.Append($"\nSteering: {_currentSteeringAngle}");
            info.Append($"\nDirection input: {_gasInput}");
            info.Append($"\nTurn input: {_turnInput}");

            infoText.text = info.ToString();
        }

        private void SaveMovingData()
        {
            CheckGrounded();

            var carTransform = transform;
            _movingData = new CarMovingData(carTransform.position, carTransform.rotation, _carIsGrounded);
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

        public void Reinitialize()
        {
            _currentMotorTorque = 0;
            _currentSpeed = 0;
            _currentSteeringAngle = 0;
            
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

        public void StopCar()
        {
            foreach (var axle in axles)
            {
                axle.StopWheels();
            }
        }

        public void ChangeMovementStatus()
        {
            _canMove = !_canMove;
        }

        public void IsPaused()
        {
            if (_canMove)
            {
                _canMove = false;
                carRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                _canMove = true;
                carRigidbody.constraints = RigidbodyConstraints.None;
            }
        }
    }
}