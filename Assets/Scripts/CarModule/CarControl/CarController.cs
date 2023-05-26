using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServiceLocatorModule;
using Services.Input;
using TMPro;
using UnityEngine;

namespace CarModule.CarControl
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI infoText;

        // car config
        [SerializeField] private List<CarAxle> axles;
        [SerializeField] private Rigidbody carRigidbody;
        [SerializeField] private Transform centerOfMass;
        [SerializeField] private AnimationCurve steeringCurve;

        private CarConfig _config;

        // calculates
        private float _currentMotorTorque;
        private bool _handbrakeActivated;
        private int _stepAtChangingSpeed;
        private float _currentSpeed;
        private float _currentSteeringAngle;
        private float _currentSlipAngle;
        
        // input
        private float _brakeInput;
        private float _gasInput;
        private float _turnInput;

        private CancellationTokenSource _cancellationTokenGainMotorTorque;
        private CancellationTokenSource _cancellationTokenLoseMotorTorque;
        private CancellationTokenSource _cancellationTokenChangeDirection;

        // additional services
        private InputService _input;
        private Coroutine _gainSpeedCoroutine;
        private Coroutine _decreaseSpeedCoroutine;
        private int _carSpeed;


        private void Start()
        {
            _input = ServiceLocator.Instance.GetService<InputService>();
            /*_input.HandbrakeStateChanged += OnHandbrakeSwitched;
            _input.DirectionChanged += OnDirectionChanged;
            _input.CarMoveCanceled += OnMoveCanceled;*/

            carRigidbody.centerOfMass = centerOfMass.position;

            _stepAtChangingSpeed =
                Convert.ToInt32(_config.MaxMotorTorque /
                                (_config.AccelerationTimeInMilliseconds /
                                 _config.OneStepTimeWaitAtDelayAccelerationInMilliseconds));

            _cancellationTokenGainMotorTorque = new CancellationTokenSource();
            _cancellationTokenLoseMotorTorque = new CancellationTokenSource();
            _cancellationTokenChangeDirection = new CancellationTokenSource();
        }

        private void Update()
        {
            _currentSpeed = carRigidbody.velocity.magnitude;
            CheckInput();
            UpdateWheelMesh();
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
            _currentSteeringAngle = _turnInput * steeringCurve.Evaluate(_currentSpeed);
            /*_currentSteeringAngle +=
                Vector3.SignedAngle(transform.forward, carRigidbody.velocity + transform.forward, Vector3.up);
            _currentSteeringAngle = Mathf.Clamp(_currentSteeringAngle,-90, 90);*/

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
            axles[0].ApplyBrakePower(_brakeInput * _config.HandbrakeForce * 0.7f);
            axles[1].ApplyBrakePower(_brakeInput * _config.HandbrakeForce * 0.3f);
        }
        
        private void ApplyMotorTorque()
        {
            foreach (var axle in axles)
            {
                if (axle.hasMotor)
                {
                    _currentMotorTorque = _config.MaxMotorTorque * _gasInput;
                    axle.SetMotorTorque(_currentMotorTorque);
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
            _currentSlipAngle = Vector3.Angle(forward, carRigidbody.velocity - forward);
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
            
            /*_currentSlipAngle = Vector3.Angle(forward, carRigidbody.velocity - forward);

            if (_currentSlipAngle < 120f)
            {
                if (_gasInput < 0)
                {
                    _brakeInput = Mathf.Abs(_gasInput);
                    _gasInput = 0;
                }
                else
                {
                    _brakeInput = 0;
                }
            }
            else
            {
                _brakeInput = 0;
            }*/
        }
        
        private void ShowNewInfo()
        {
            StringBuilder info = new StringBuilder();
            info.Append($"Motor Torque: {_currentMotorTorque}");
            info.Append($"\nRB speed: {_currentSpeed}");
            info.Append($"\nSteering: {_currentSteeringAngle}");
            info.Append($"\nDirection: {_gasInput}");

            infoText.text = info.ToString();
        }
        
        
        public void Initialize(CarConfig config)
        {
            _config = config;
        }

        

        private void OnHandbrakeSwitched(bool handbrakeActive)
        {
            _handbrakeActivated = handbrakeActive;
        }

        private async void OnDirectionChanged()
        {
            float newDirection = _input.GetDirection();
            if (_gasInput > 0.5 && newDirection < -0.5 || _gasInput < -0.5 && newDirection > 0.5)
            {
                _cancellationTokenGainMotorTorque.Cancel(false);

                _cancellationTokenLoseMotorTorque = new CancellationTokenSource();
                await LoseSpeedAsync(_cancellationTokenLoseMotorTorque.Token);
            }

            _gasInput = _input.GetDirection();
            _cancellationTokenLoseMotorTorque.Cancel();
            _cancellationTokenGainMotorTorque = new CancellationTokenSource();
            await GainSpeedAsync(_cancellationTokenGainMotorTorque.Token);
        }


        private async void OnMoveCanceled()
        {
            _gasInput = 0;

            /*_cancellationTokenGainMotorTorque.Cancel(false);
            
            _cancellationTokenLoseMotorTorque = new CancellationTokenSource();
            await LoseSpeedAsync(_cancellationTokenLoseMotorTorque.Token);*/
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
            if (axle.canSteer)
            {
                float steering = _config.MaxSteeringAngle * _input.GetTurn();

                axle.leftWheelCollider.steerAngle = steering;
                axle.rightWheelCollider.steerAngle = steering;
            }
        }

        private void StartMotor(CarAxle axle)
        {
            if (axle.hasMotor)
            {
                if (carRigidbody.velocity.magnitude > _config.MaxSpeed)
                {
                    axle.leftWheelCollider.motorTorque = 0;
                    axle.rightWheelCollider.motorTorque = 0;
                }
                else
                {
                    float motor = _currentMotorTorque * _gasInput;

                    axle.leftWheelCollider.motorTorque = motor;
                    axle.rightWheelCollider.motorTorque = motor;
                }
            }
        }

        private void ActivateHandbrake(CarAxle axle)
        {
            if (!axle.hasHandbrake && _handbrakeActivated)
            {
                axle.leftWheelCollider.brakeTorque = _config.ResistanceForce;
                axle.rightWheelCollider.brakeTorque = _config.ResistanceForce;
            }
            else if (axle.hasHandbrake && _handbrakeActivated)
            {
                axle.leftWheelCollider.brakeTorque = _config.HandbrakeForce;
                axle.rightWheelCollider.brakeTorque = _config.HandbrakeForce;
            }
            else if (axle.hasHandbrake && !_handbrakeActivated)
            {
                axle.leftWheelCollider.brakeTorque = 0;
                axle.rightWheelCollider.brakeTorque = 0;
            }
        }

        private void ShowInfo()
        {
            if (!infoText)
                return;

            _carSpeed = Convert.ToInt32(Math.Abs(
                (2 * Mathf.PI * axles[0].leftWheelCollider.radius * axles[0].leftWheelCollider.rpm * 60) / 1000));
            infoText.text = $"Speed: {_carSpeed}";
            infoText.text += $"\nTrue Speed: {Convert.ToInt32(carRigidbody.velocity.magnitude)}";
            infoText.text += $"\nrpm: {axles[0].leftWheelCollider.rpm}";
            infoText.text += $"\nMotor Torque: {_currentMotorTorque}";
            infoText.text += $"\nDirection: {_gasInput}";
        }

        private void AddResistanceForce()
        {
            foreach (var axle in axles)
            {
                axle.leftWheelCollider.brakeTorque = _config.ResistanceForce;
                axle.rightWheelCollider.brakeTorque = _config.ResistanceForce;
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

        private async Task GainSpeedAsync(CancellationToken token)
        {
            try
            {
                if (token.IsCancellationRequested)
                    return;

                StopBrake();

                while (!token.IsCancellationRequested && _currentMotorTorque < _config.MaxMotorTorque)
                {
                    if (token.IsCancellationRequested)
                        return;

                    await Task.Delay(_config.OneStepTimeWaitAtDelayAccelerationInMilliseconds, token);

                    if (token.IsCancellationRequested)
                        return;

                    _currentMotorTorque += _stepAtChangingSpeed;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        private async Task LoseSpeedAsync(CancellationToken token)
        {
            try
            {
                if (token.IsCancellationRequested)
                    return;

                StopBrake();

                while (!token.IsCancellationRequested && _currentMotorTorque > 0)
                {
                    if (token.IsCancellationRequested)
                        return;

                    await Task.Delay(_config.OneStepTimeWaitAtDelayAccelerationInMilliseconds, token);

                    if (token.IsCancellationRequested)
                        return;

                    _currentMotorTorque -= _stepAtChangingSpeed;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}