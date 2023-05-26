using System;
using System.Threading.Tasks;
using CarModule.CarComponents;
using CarModule.CarControl;
using UnityEngine;

namespace CarModule
{
    public class Car : MonoBehaviour, IDamageable
    {
        [SerializeField] private CarConfigSo carConfigSo;
        [SerializeField] private CarController carController;

        private CarConfig _config;
        private IDamageable _damageable;
        private Health _health;

        public void Awake()
        {
            _config = carConfigSo.GetConfig();
            
            _health = new Health(_config.MaxHealth);
            _damageable = new SimpleDamageable(_health);
            
            carController.Initialize(_config);
        }

        public void MakeDamage(int damage)
        {
            _damageable.MakeDamage(damage);
            
            Immortal();
        }

        private async void Immortal()
        {
            _damageable = new NonDamageable();

            await Task.Delay(_config.ImmortalTimeInMilliseconds);

            _damageable = new SimpleDamageable(_health);
        }
    }
}