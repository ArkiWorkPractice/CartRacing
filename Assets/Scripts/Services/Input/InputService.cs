using System;
using ServiceLocatorModule;
using ServiceLocatorModule.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Services.Input
{
    public class InputService : MonoBehaviour, IDisposable, IService
    {
        [SerializeField] private float smoothInputSpeed;
        
        private ControlActions _actions;

        private Vector2 _currentDirection;
        private Vector2 _directionInput;
        private Vector2 _smoothDirectionInputVelocity;
        
        public Vector2 Direction => _currentDirection;
        
        private bool _isHandbrakeActivated;
        
        public event Action<bool> HandbrakeStateChanged;
        public event Action DirectionChanged;
        public event Action CarMoveCanceled;
        public event Action EscapePressed;
        public event Action RespawnClicked;

        private void Awake()
        {
            _actions = new ControlActions();
            _actions.Enable();
            ServiceLocator.Instance.RegisterService(this);

            _directionInput = new Vector2(0, 0);
            
            SubscribePlayerInputs();
            SubscribeUIInputs();
        }
        
        
        private void Update()
        {
            _currentDirection = Vector2.SmoothDamp(_currentDirection, _directionInput, ref _smoothDirectionInputVelocity, smoothInputSpeed);
        }

        private void OnDisable()
        {
            Dispose();
            ServiceLocator.Instance.UnregisterService<InputService>();
        }
        
        private void SubscribeUIInputs()
        {
            _actions.ui.escape.performed += OnEscapePerformed;
        }

        private void OnEscapePerformed(InputAction.CallbackContext obj)
        {
            EscapePressed?.Invoke();
        }

        private void SubscribePlayerInputs()
        {
            _actions.player.direction.performed += OnDirectionPerformed;
            _actions.player.direction.canceled += OnDirectionCanceled;
            
            _actions.player.turn.performed += OnTurnPerformed;
            _actions.player.turn.canceled += OnTurnCanceled;

            _actions.player.handbrake.performed += OnHandbrakePerformed;
            _actions.player.handbrake.canceled += OnHandBrakeCanceled;

            _actions.player.respawn.performed += OnRespawnClicked;
        }

        private void OnRespawnClicked(InputAction.CallbackContext obj)
        {
            RespawnClicked?.Invoke();
        }

        private void OnTurnCanceled(InputAction.CallbackContext obj)
        {
            _directionInput.y = 0;
        }

        private void OnDirectionCanceled(InputAction.CallbackContext obj)
        {
            _directionInput.x = 0;
            CarMoveCanceled?.Invoke();
        }

        private void OnTurnPerformed(InputAction.CallbackContext obj)
        {
            _directionInput.y = obj.ReadValue<float>();;
        }

        private void OnDirectionPerformed(InputAction.CallbackContext obj)
        {
            _directionInput.x = obj.ReadValue<float>();
            DirectionChanged?.Invoke();
        }

        private void OnHandbrakePerformed(InputAction.CallbackContext obj)
        {
            _isHandbrakeActivated = true;
            HandbrakeStateChanged?.Invoke(true);
        }

        private void OnHandBrakeCanceled(InputAction.CallbackContext obj)
        {
            _isHandbrakeActivated = false;
            HandbrakeStateChanged?.Invoke(false);
        }
        
        public bool GetHandbrakeStatus() => _isHandbrakeActivated;

        public void Dispose()
        {
            _actions.Disable();
            _actions?.Dispose();
        }

        
    }
}
