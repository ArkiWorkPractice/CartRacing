using System;
using Configs;
using ServiceLocatorModule;
using ServiceLocatorModule.Interfaces;
using UnityEngine;

namespace Services
{
    [Serializable]
    public class ConfigsProvider : IService
    {
        public ObstacleSpawnerConfig ObstacleSpawnerConfig => obstacleSpawnerConfig;

        [SerializeField] private ObstacleSpawnerConfig obstacleSpawnerConfig;

        public void Awake()
        {
            ServiceLocator.Instance.RegisterService(this);
        }
    }
}