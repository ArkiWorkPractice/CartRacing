using System;
using Factories;
using Obstacles.Abstract;
using ServiceLocatorModule;
using UnityEngine;

namespace ObstacleSpawn
{
    [RequireComponent(typeof(SphereCollider))]
    public class ObstacleSpawnPoint : MonoBehaviour
    {
        [SerializeField] private int minDamageLimit;
        [SerializeField] private int maxDamageLimit;

         private Obstacle _placedObstacle;

        public event Action<ObstacleSpawnPoint> PlayerIsNear;
        public event Action<ObstacleSpawnPoint> NeedToReturn;
        
        public (int,int) GetDamageLimit() => (minDamageLimit,maxDamageLimit);

        public void SetObstacle(Obstacle obstacle)
        {
            _placedObstacle = obstacle;
            _placedObstacle.NeedToReturn += ReturnObstacle;
        }

        private void ReturnObstacle()
        {
            NeedToReturn?.Invoke(this);
        }

        public void RemoveObstacleOnPoint()
        {
            _placedObstacle = null;
        }

        public Obstacle GetObstacleOnPoint() => _placedObstacle;

        private void OnDisable()
        {
            NeedToReturn?.Invoke(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerIsNear?.Invoke(this);
        }

        private void OnTriggerExit(Collider other)
        {
            ReturnObstacle();
        }
    }
}