using System;
using System.Threading.Tasks;
using CarModule.CarComponents;
using UnityEngine;

namespace CarModule
{
    public class Car : MonoBehaviour, IDamageable
    {
        
        private IDamageable _damageable;
        private Health _health;
        
        
        private const int ImmortalTime = 5000; 
        private const int MaxHealth = 100; 

        public void Awake()
        {
            _damageable = new SimpleDamageable(_health);
            _health = new Health(MaxHealth);
        }

        public void MakeDamage(int damage)
        {
            _damageable.MakeDamage(damage);
            
            Immortal();
        }

        private async void Immortal()
        {
            _damageable = new NonDamageable();

            await Task.Delay(ImmortalTime);

            _damageable = new SimpleDamageable(_health);
        }
    }
}