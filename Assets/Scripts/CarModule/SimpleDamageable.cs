using CarModule.CarComponents;

namespace CarModule
{
    public class SimpleDamageable : IDamageable
    {
        private readonly Health _health;

        public SimpleDamageable(Health health)
        {
            _health = health;
        }

        public void MakeDamage(int damage)
        {
            _health.DecreaseHealth(damage);
        }
    }
}