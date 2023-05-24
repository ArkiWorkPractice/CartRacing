using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Services.Input
{
    public class InputService : IDisposable
    {
        private readonly CarControlActions _actions;

        private float _direction;
        private float _turn;
        
        public event Action<bool> HandbrakeStateChanged;
        public event Action<bool> DirectionChanged;
        
        public InputService()
        {
            _actions = new CarControlActions();
            _actions.Enable();
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
            DirectionChanged?.Invoke(false);
        }

        private void OnTurnPerformed(InputAction.CallbackContext obj)
        {
            _turn = obj.ReadValue<float>();
        }

        private void OnDirectionPerformed(InputAction.CallbackContext obj)
        {
            _direction = obj.ReadValue<float>();
            DirectionChanged?.Invoke(true);
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
        

        ~InputService()
        {
            Dispose();
        }

        public void Dispose()
        {
            _actions.Disable();
            _actions?.Dispose();
        }
    }
}
