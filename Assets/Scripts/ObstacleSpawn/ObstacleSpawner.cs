using System;
using System.Collections.Generic;
using Factories;
using Obstacles.Abstract;
using ServiceLocatorModule;
using Services;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ObstacleSpawn
{
    public class ObstacleSpawner : MonoBehaviour
    {
        private readonly Obstacle[] _obstacles;
        private int _quantityForOneObject;
        private readonly ObstacleFactory _obstacleFactory;

        public ObstacleSpawner(ObstacleSpawnPoint[] spawnPoints)
        {
            _obstacles = ServiceLocator.Instance.GetService<PrefabsProvider>().GetObstacles();
            _obstacleFactory = ServiceLocator.Instance.GetService<ObstacleFactory>();
            
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i].PlayerIsNear += SpawnObstacles;
                spawnPoints[i].NeedToReturn += RemoveObstacle;
            }
        }
        private bool CheckSpawnPointDamageLimits(Obstacle obstacle, ObstacleSpawnPoint spawnPoint)
        {
            var (min, max) = spawnPoint.GetDamageLimit();
            return obstacle.GetDamage() >= min && obstacle.GetDamage() <= max;
        }

        private void SpawnObstacles(ObstacleSpawnPoint spawnPoint)
        {
            int randomObstacleIndex = Random.Range(0, _obstacles.Length);

            while (!CheckSpawnPointDamageLimits(_obstacles[randomObstacleIndex], spawnPoint))
            {
                randomObstacleIndex = Random.Range(0, _obstacles.Length);
            }

            _obstacleFactory.Create(randomObstacleIndex,spawnPoint);
        }

        private void RemoveObstacle(ObstacleSpawnPoint spawnPoint)
        {
            Obstacle obstacle = spawnPoint.GetObstacleOnPoint();
            if (obstacle == null) return;
            _obstacleFactory.ReturnToObjectPool(spawnPoint.GetObstacleOnPoint());
            spawnPoint.RemoveObstacleOnPoint();
        }
    }
}