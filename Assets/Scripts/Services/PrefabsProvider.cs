using System;
using System.Linq;
using Levels;
using Obstacles.Abstract;
using ScriptableObjects;
using ServiceLocatorModule;
using ServiceLocatorModule.Interfaces;
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

        private void Awake()
        {
            ServiceLocator.Instance.RegisterService(this);
        }
    }
}