using System;
using System.Collections.Generic;
using ServiceLocatorModule;
using ServiceLocatorModule.Interfaces;
using UnityEngine;

namespace DefaultNamespace
{
    public class PrefabProvider : MonoBehaviour, IService
    {
        [SerializeField] private List<Obstacle> obstacles;

        public List<Obstacle> GetObstacles() => obstacles;

        public void Awake()
        {
            ServiceLocator.Instance.RegisterService(this);
        }
    }
}