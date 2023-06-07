using System;
using System.Linq;
using CarModule;
using Levels;
using Obstacles.Abstract;
using ScriptableObjects;
using ServiceLocatorModule;
using ServiceLocatorModule.Interfaces;
using UI;
using UnityEngine;

namespace Services
{
    [Serializable]
    public class PrefabsProvider : IService
    {
        [SerializeField] private PrefabsContainerSO prefabsContainerSo;

        public Obstacle[] GetObstacles() => prefabsContainerSo.Obstacles;
        public Level[] GetLevels() => prefabsContainerSo.Levels;
        public Level GetLevel(int index) => prefabsContainerSo.Levels.First(l => l.GetLevelId()==index);
        public UIController GetUIController() => prefabsContainerSo.UIPrefab;

        public Car GetCarPrefab() => prefabsContainerSo.CarPrefab;

        private void Awake()
        {
            ServiceLocator.Instance.RegisterService(this);
        }
    }
}