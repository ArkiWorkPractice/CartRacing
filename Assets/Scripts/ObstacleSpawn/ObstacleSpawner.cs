using System;
using System.Collections.Generic;
using DefaultNamespace;
using Factory;
using Obstacles.Abstract;
using ServiceLocatorModule;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ObstacleSpawn
{
    public class ObstacleSpawner : MonoBehaviour
    {
        private Obstacle[] _obstacles;
        [SerializeField] private ObstacleSpawnPoint[] spawnPoints;
        [SerializeField] private Transform playerPosition;
        [SerializeField] private int quantityForOneObject;
        private ObstacleFactory _obstacleFactory; 

        private void Start()
        {
            _obstacles = ServiceLocator.Instance.GetService<PrefabProvider>().GetObstacles();
            _obstacleFactory = new ObstacleFactory(_obstacles,quantityForOneObject);
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i].PlayerIsNear += SpawnObstacles;
                spawnPoints[i].NeedToReturn += RemoveObstacle;
            }
        }

        /*private void CheckDistanceToSpawnPoint()
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (!spawnPoints[i].GetSpawnPointStatus() && Vector3.Distance(spawnPoints[i].transform.position, playerPosition.position) < 150f)
                {
                     SpawnObstacles(spawnPoints[i]);
                }
                else if (spawnPoints[i].GetSpawnPointStatus() && Vector3.Distance(spawnPoints[i].transform.position, playerPosition.position) > 200f)
                {
                    RemoveObstacle(spawnPoints[i]);
                }
            }
        }*/

        /*private void Update()
        {
            CheckDistanceToSpawnPoint();
        }*/

        private bool CheckSpawnPointDamageLimits(Obstacle obstacle, ObstacleSpawnPoint spawnPoint)
        {
            var damageLimit = spawnPoint.GetDamageLimit();
            if (obstacle.GetDamage() >= damageLimit.Item1 && obstacle.GetDamage() <= damageLimit.Item2)
            {
                return true;
            }

            return false;
        }

        private void SpawnObstacles(ObstacleSpawnPoint spawnPoint)
        {
            if (spawnPoint.GetSpawnPointStatus())
            {
                return;
            }
            int randomObstacleIndex = Random.Range(0, _obstacles.Length);

            while (!CheckSpawnPointDamageLimits(_obstacles[randomObstacleIndex], spawnPoint))
            {
                randomObstacleIndex = Random.Range(0, _obstacles.Length);
            }

            spawnPoint.ChangeActiveStatus();
            _obstacleFactory.Create(randomObstacleIndex,spawnPoint);
        }

        private void RemoveObstacle(ObstacleSpawnPoint spawnPoint)
        {
            _obstacleFactory.ReturnToObjectPool(spawnPoint.GetObstacleOnPoint());
            spawnPoint.RemoveobstacleOnPoint();
        }
    }
}