using UnityEngine;

namespace Testing.LevelDesignModuleTests.Car
{
    public class Car : MonoBehaviour, IDamageable
    {
        [SerializeField] private int health;
        public void MakeDamage(int value)
        {
            health -= value;
        }
    }
}
