using ObstacleSpawn;
using UnityEngine;

namespace Levels
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private int levelId;
        [SerializeField] private ObstacleSpawnPoint[] spawnPoints;
        [SerializeField] private Transform playerSpawnPosition;

        public int GetLevelId() => levelId;

        public Transform GetPlayerPosition() => playerSpawnPosition;

        public ObstacleSpawnPoint[] GetSpawnPoints() => spawnPoints;
    }
}
