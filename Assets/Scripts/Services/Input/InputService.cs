using System;
using ServiceLocatorModule;
using ServiceLocatorModule.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Services.Input
{
    public class InputService : MonoBehaviour, IDisposable, IService
    {
        private ControlActions _actions;

        private float _direction;
        private float _turn;
        
        public event Action<bool> HandbrakeStateChanged;
        public event Action DirectionChanged;
        public event Action CarMoveCanceled;
        public event Action EscapePressed;

        private void Awake()
        {
            _actions = new ControlActions();
            _actions.Enable();
            ServiceLocator.Instance.RegisterService(this);
            
            SubscribePlayerInputs();
            SubscribeUIInputs();
        }
        
        private void OnEnable()
        {
            
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
        }

        private void OnTurnCanceled(InputAction.CallbackContext obj)
        {
            _turn = 0;
        }

        private void OnDirectionCanceled(InputAction.CallbackContext obj)
        {
            _direction = 0;
            CarMoveCanceled?.Invoke();
        }

        private void OnTurnPerformed(InputAction.CallbackContext obj)
        {
            _turn = obj.ReadValue<float>();
        }

        private void OnDirectionPerformed(InputAction.CallbackContext obj)
        {
            _direction = obj.ReadValue<float>();
            DirectionChanged?.Invoke();
        }

        private void OnHandbrakePerformed(InputAction.CallbackContext obj)
        {
            HandbrakeStateChanged?.Invoke(true);
        }

        private void OnHandBrakeCanceled(InputAction.CallbackContext obj)
        {
            HandbrakeStateChanged?.Invoke(false);
        }

        public float GetDirection() => _direction;
        public float GetTurn() => _turn;

       

        public void Dispose()
        {
            _actions.Disable();
            _actions?.Dispose();
        }
    }
}
