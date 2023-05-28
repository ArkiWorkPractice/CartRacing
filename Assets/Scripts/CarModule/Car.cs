using System;
using System.Threading;
using System.Threading.Tasks;
using CarModule.CarComponents;
using CarModule.CarControl;
using Models.CarModule;
using ServiceLocatorModule;
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

        private CancellationTokenSource _immortalCancellationTokenSource;

        public void Start()
        {
            _config = carConfigSo.GetConfig();

            InitializeCar();

            carController.Initialize(_config);

            var saver = ServiceLocator.Instance.GetService<CarSaver>();
            saver.Initialize(this, _config.DelayBetweenSaving);
            saver.StartSaving();
        }

        private void InitializeCar()
        {
            _health = new Health(_config.MaxHealth);
            _damageable = new SimpleDamageable(_health);
            
            
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

        private void OnDisable()
        {
            CancelImmortal();
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

        public void Reset()
        {
            InitializeCar();
            carController.Reset();
        }

        public void ResetPosition(CarMovingData data)
        {
            var carTransform = transform;
            carTransform.position = data.Position;
            carTransform.rotation = data.Rotation;
        }
    }
}