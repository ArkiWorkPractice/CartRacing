using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = System.Numerics.Vector2;

namespace Services.Input
{
    public class InputService : IDisposable
    {
        private readonly CarControlActions _actions;
        private Vector2 _moveDirection;

        public InputService()
        {
            _actions = new CarControlActions();
            _actions.Enable();
            _actions.player.move.performed += OnMovementPerformed;
            _actions.player.move.canceled += OnMovementCanceled;
        }

        public Vector2 GetDirection() => _moveDirection;
        
        private void OnMovementPerformed(InputAction.CallbackContext action)
        {
            _moveDirection = action.ReadValue<Vector2>();
        }
        
        private void OnMovementCanceled(InputAction.CallbackContext action)
        {
            _moveDirection = Vector2.Zero;
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
