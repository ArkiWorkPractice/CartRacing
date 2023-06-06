using System.Collections;
using Models.CarModule;
using ServiceLocatorModule;
using ServiceLocatorModule.Interfaces;
using Services.Input;
using UnityEngine;

namespace CarModule
{
    public class CarSaver : MonoBehaviour, IService
    {
        private Car _car;
        private int _delayBetweenSaving;

        private CarMovingData _lastPosition;

        private Coroutine _saverCoroutine;
        private bool _savingInProcess;

        private InputService _inputService;

        private void OnDisable()
        {
            StopSaving();
        }
        
        public void Initialize(Car car, int delayBetweenSaving)
        {
            _car = car;
            _delayBetweenSaving = delayBetweenSaving;
            _savingInProcess = false;
            _lastPosition = _car.GetMovingData();
            _inputService = ServiceLocator.Instance.GetService<InputService>();

            _inputService.RespawnClicked += RestoreData;
        }

        private void RestoreData()
        {
            StartCoroutine(RespawnCar());

            Debug.Log("restored");
        }

        public void StartSaving()
        {
            if (_savingInProcess) return;
            
            _savingInProcess = true;
            _saverCoroutine = StartCoroutine(Saving());
        }

        public void StopSaving()
        {
            if (_saverCoroutine != null)
            {
                StopCoroutine(_saverCoroutine);
                _savingInProcess = false;
            }

            _saverCoroutine = null;
        }

        private IEnumerator Saving()
        {
            while (_savingInProcess)
            {
                var data = _car.GetMovingData();
                if (data.IsGrounded)
                {
                    _lastPosition = data.Copy();
                }

                var delay = _delayBetweenSaving / 1000f;
                yield return new WaitForSeconds(delay);
            }
        }

        private IEnumerator RespawnCar()
        {
            _car.StopCar();
            _car.ResetPosition(_lastPosition);
            yield return new WaitForSeconds(0.5f);
            _car.ReleaseCar();
        }

        public void Dispose()
        {
            _inputService.RespawnClicked -= RestoreData;
            StopSaving();
        }
    }
}