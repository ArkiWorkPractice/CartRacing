using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Services.Input
{
    public class InputService : IDisposable
    {
        private readonly CarControlActions _actions;
        private Vector2 _moveDirection;
        public event Action<bool> HandbrakeStateChanged;
        public event Action<bool> MoveStateChanged;
        
        public InputService()
        {
            _actions = new CarControlActions();
            _actions.Enable();
            _actions.player.move.performed += OnMovementPerformed;
            _actions.player.move.canceled += OnMovementCanceled;

            _actions.player.handbrake.performed += OnHandbrakePerformed;
            _actions.player.handbrake.canceled += OnHandBrakeCanceled;

        }

        private void OnHandbrakePerformed(InputAction.CallbackContext obj)
        {
            HandbrakeStateChanged?.Invoke(true);
        }

        private void OnHandBrakeCanceled(InputAction.CallbackContext obj)
        {
            HandbrakeStateChanged?.Invoke(false);
        }

        public Vector2 GetDirection() => _moveDirection;

        private void OnMovementPerformed(InputAction.CallbackContext action)
        {
            _moveDirection = action.ReadValue<Vector2>();
            MoveStateChanged?.Invoke(true);
        }
        
        private void OnMovementCanceled(InputAction.CallbackContext action)
        {
            _moveDirection = Vector2.zero;
            MoveStateChanged?.Invoke(false);
        }

        ~InputService()
        {
            Dispose();
        }

        public void Dispose()
        {
            _actions.player.move.performed -= OnMovementPerformed;
            _actions.player.move.canceled -= OnMovementCanceled;
            _actions.Disable();
            _actions?.Dispose();
        }
    }
}
