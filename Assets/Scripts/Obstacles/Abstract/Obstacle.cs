using System;
using Obstacles.Interface;
using Test;
using UnityEngine;

namespace Obstacles.Abstract
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private int damageAmount;

        public event Action NeedToReturn; 

        public int GetDamage() => damageAmount;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.MakeDamage(damageAmount);

                if (this is IDestroyable)
                {
                    NeedToReturn?.Invoke();
                }
            }
        }

        private void OnDisable()
        {
            NeedToReturn?.Invoke();
        }
    }
}