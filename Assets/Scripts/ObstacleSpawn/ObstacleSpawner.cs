using System;
using System.Collections.Generic;
using DefaultNamespace;
using ServiceLocatorModule;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ObstacleSpawn
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private List<Obstacle> obstacles;
        [SerializeField] private ObstacleSpawnPoint[] spawnPoints;

        private void Awake()
        {
            spawnPoints = GetComponentsInChildren<ObstacleSpawnPoint>();
        }

        private void Start()
        {
            obstacles = ServiceLocator.Instance.GetService<PrefabProvider>().GetObstacles();
            SpawnObstacles();
        }

        public void SpawnObstacles()
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                Obstacle obj = Instantiate(obstacles[Random.Range(0, obstacles.Count)], spawnPoints[i].transform);
                var rotation = obj.transform.rotation;
                obj.gameObject.transform.rotation = Quaternion.Euler(rotation.x, Random.Range(0f, 180f), rotation.z);
            }
        }
        
    }
}