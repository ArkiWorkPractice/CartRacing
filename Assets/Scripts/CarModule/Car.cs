using System;
using System.Threading;
using System.Threading.Tasks;
using CarModule.CarComponents;
using CarModule.CarControl;
using Models.CarModule;
using UnityEngine;

namespace CarModule
{
    [RequireComponent(typeof(CarController))]
    public class Car : MonoBehaviour, IDamageable
    {
        [SerializeField] private CarConfigSo carConfigSo;
        [SerializeField] private CarController carController;
        [SerializeField] private CarSaver carSaver;

        private CarConfig _config;
        private IDamageable _damageable;
        private Health _health;

        public event Action<int> HealthChanged;
        public event Action Died;

        private CancellationTokenSource _immortalCancellationTokenSource;

        public void Start()
        {
            InitializeCar();

            carSaver.Initialize(this, _config.DelayBetweenSaving);
            carSaver.StartSaving();
        }

        private void InitializeCar()
        {
            _config = carConfigSo.GetConfig();
            
            _health = new Health(_config.MaxHealth);
            _health.Died += OnDied;
            _health.HealthValueChanged += OnHealthChanged;
            
            _damageable = new SimpleDamageable(_health);
            carController.Initialize(_config);
        }

        private void OnHealthChanged(int value)
        {
            HealthChanged?.Invoke(value);
        }

        private void OnDied()
        {
            Died?.Invoke();
        }

        public void MakeDamage(int damage)
        {
            _damageable.MakeDamage(damage);
            _immortalCancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => Immortal(_immortalCancellationTokenSource.Token));
        }

        private async Task Immortal(CancellationToken token)
        {
            _damageable = new NonDamageable();

            await Task.Delay(_config.ImmortalTimeInMilliseconds, token);

            _damageable = new SimpleDamageable(_health);
        }

        

        private void CancelImmortal()
        {
            _immortalCancellationTokenSource?.Cancel(false);
            _damageable = new SimpleDamageable(_health);
        }

        public CarMovingData GetMovingData()
        {
            return carController.MovingData;
        }
        
        private void OnDisable()
        {
            CancelImmortal();
        }

        public void Reinitialize()
        {
            InitializeCar();
            carController.Reinitialize();
        }

        public void ResetPosition(CarMovingData data)
        {
            var carTransform = transform;
            carTransform.position = data.Position;
            carTransform.rotation = data.Rotation;
        }
    }
}