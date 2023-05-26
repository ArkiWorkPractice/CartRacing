using System;
using Obstacles.Abstract;
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

        private bool _isActive;
        public (int,int) GetDamageLimit() => (minDamageLimit,maxDamageLimit);

        public bool GetSpawnPointStatus() => _isActive;

        public void SetObstacle(Obstacle obstacle)
        {
            _placedObstacle = obstacle;
            _placedObstacle.NeedToReturn += ReturnObstacle;
        }

        private void ReturnObstacle()
        {
            NeedToReturn?.Invoke(this);
        }

        public void RemoveobstacleOnPoint()
        {
            _placedObstacle = null;
        }

        public Obstacle GetObstacleOnPoint() => _placedObstacle;

        public void ChangeActiveStatus()
        {
            _isActive = !_isActive;
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