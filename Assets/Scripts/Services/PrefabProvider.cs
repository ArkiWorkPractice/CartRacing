using System;
using System.Collections.Generic;
using Obstacles.Abstract;
using ServiceLocatorModule;
using ServiceLocatorModule.Interfaces;
using UnityEngine;

namespace DefaultNamespace
{
    public class PrefabProvider : MonoBehaviour, IService
    {
        [SerializeField] private Obstacle[] obstacles;

        public Obstacle[] GetObstacles() => obstacles;

        public void Awake()
        {
            ServiceLocator.Instance.RegisterService(this);
        }
    }
}