using System;
using Levels;
using Obstacles.Abstract;
using ScriptableObjects;
using ServiceLocatorModule;
using ServiceLocatorModule.Interfaces;
using UnityEngine;

namespace Services
{
    [Serializable]
    public class PrefabProvider : IService
    {
        [SerializeField] private PrefabsContainerSO prefabsContainerSo;

        public Obstacle[] GetObstacles() => prefabsContainerSo.Obstacles;
        public Level[] GetLevels() => prefabsContainerSo.Levels;
        public Level GetLevel(int index) => prefabsContainerSo.Levels[index];

        public void Awake()
        {
            ServiceLocator.Instance.RegisterService(this);
        }
    }
}