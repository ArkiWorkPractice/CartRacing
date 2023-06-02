using System;
using System.Threading;
using System.Threading.Tasks;
using CarModule.CarComponents;
using CarModule.CarControl;
using EventBusModule;
using EventBusModule.Interfaces;
using Models.CarModule;
using ServiceLocatorModule;
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
        private EventBus _eventBus;

        private bool _isImmortal;
        private float _currentImmortalTime;

        public event Action<int> HealthChanged;
        public event Action Died;

        private void Update()
        {
            ImmortalCheck();
        }

        private void ImmortalCheck()
        {
            if (_isImmortal)
            {
                _currentImmortalTime -= Time.deltaTime;

                if (_currentImmortalTime <= 0) CancelImmortal();
            }
        }

        public void InitializeCar()
        {
            _config = carConfigSo.GetConfig();

            _health = new Health(_config.MaxHealth);
            _health.Died += OnDied;
            _health.HealthValueChanged += OnHealthChanged;

            _damageable = new SimpleDamageable(_health);
            carController.Initialize(_config);
            _eventBus = ServiceLocator.Instance.GetService<EventBus>();
            _isImmortal = false;
            carSaver.Initialize(this, _config.DelayBetweenSaving);
            carSaver.StartSaving();
        }

        private void OnHealthChanged(int value)
        {
            HealthChanged?.Invoke(value);
            _eventBus.Raise( EventBusDefinitions.UpdateHealthValueActionKey, new SingleIntParameterEventBusArgs(_health.CurrentHealth));
        }

        private void OnDied()
        {
            StopCar();
            Died?.Invoke();
            _eventBus.Raise(EventBusDefinitions.EndGameActionKey, new EventBusArgs());
        }

        public void MakeDamage(int damage)
        {
            _damageable.MakeDamage(damage);

            if (!_isImmortal)
            {
                _isImmortal = true;
                _damageable = new NonDamageable();
                _currentImmortalTime = _config.ImmortalTimeInMilliseconds;
            }
        }

        private void CancelImmortal()
        {
            _damageable = new SimpleDamageable(_health);
            _isImmortal = false;
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
            carController.Reinitialize();
            InitializeCar();
        }

        public void ResetPosition(CarMovingData data)
        {
            var carTransform = transform;
            carTransform.position = data.Position;
            carTransform.rotation = data.Rotation;
        }

        public void StopCar()
        {
            carController.StopCar();
            carController.Reinitialize();
        }
    }
}