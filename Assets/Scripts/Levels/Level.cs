using System;
using EndGame;
using ObstacleSpawn;
using UnityEngine;

namespace Levels
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private int levelId;
        [SerializeField] private ObstacleSpawnPoint[] spawnPoints;
        [SerializeField] private Transform playerSpawnPosition;
        private ObstacleSpawner _obstacleSpawner;
        public int GetLevelId() => levelId;
        public Transform GetPlayerPosition() => playerSpawnPosition;
        public ObstacleSpawnPoint[] GetSpawnPoints() => spawnPoints;

        public void StartLevel()
        {
            _obstacleSpawner = new ObstacleSpawner(spawnPoints);
        }

    }
}
