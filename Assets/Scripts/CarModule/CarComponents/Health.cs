using System;
using EventBusModule;
using ServiceLocatorModule;
using UnityEngine;

namespace CarModule.CarComponents
{
    public class Health
    {
        private const int MinHealth = 0;
        private readonly int _maxHealth;
        private int _currentHealth;

        public int CurrentHealth => _currentHealth;

        public event Action<int> HealthValueChanged;
        public event Action Died;

        public Health(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = _maxHealth;
            
        }
        
        public Health(int maxHealth, int currentHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = currentHealth;
        }

        public void DecreaseHealth(int valueToSubtract)
        {
            if (valueToSubtract >= _currentHealth)
            {
                _currentHealth = MinHealth;
                HealthValueChanged?.Invoke(_currentHealth);
                Died?.Invoke();
                return;
            }
            else
            {
                _currentHealth -= valueToSubtract;
            }
            HealthValueChanged?.Invoke(_currentHealth);   
        }

        public void IncreaseHealth(int valueToAdd)
        {
            if (valueToAdd + _currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
            else
            {
                _currentHealth += valueToAdd;
            }
            
            HealthValueChanged?.Invoke(_currentHealth);
        }
    }
}