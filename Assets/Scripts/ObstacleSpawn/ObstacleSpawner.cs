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
        //private readonly ObstacleSpawnPoint[] _spawnPoints;
        private int _quantityForOneObject;
        private readonly ObstacleFactory _obstacleFactory;

        public ObstacleSpawner(ObstacleSpawnPoint[] spawnPoints)
        {
            _obstacles = ServiceLocator.Instance.GetService<PrefabProvider>().GetObstacles();
            _obstacleFactory = ServiceLocator.Instance.GetService<ObstacleFactory>();
            
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i].PlayerIsNear += SpawnObstacles;
                spawnPoints[i].NeedToReturn += RemoveObstacle;
            }
        }
        private bool CheckSpawnPointDamageLimits(Obstacle obstacle, ObstacleSpawnPoint spawnPoint)
        {
            var damageLimit = spawnPoint.GetDamageLimit();
            return obstacle.GetDamage() >= damageLimit.Item1 && obstacle.GetDamage() <= damageLimit.Item2;
        }

        private void SpawnObstacles(ObstacleSpawnPoint spawnPoint)
        {
            /*if (spawnPoint.GetSpawnPointStatus())
            {
                return;
            }*/
            int randomObstacleIndex = Random.Range(0, _obstacles.Length);

            while (!CheckSpawnPointDamageLimits(_obstacles[randomObstacleIndex], spawnPoint))
            {
                randomObstacleIndex = Random.Range(0, _obstacles.Length);
            }

            //spawnPoint.ChangeActiveStatus();
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