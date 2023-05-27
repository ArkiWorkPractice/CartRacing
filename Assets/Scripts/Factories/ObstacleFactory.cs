using ObjectPool;
using Obstacles.Abstract;
using ObstacleSpawn;
using UnityEngine;

namespace Factories
{
    public class ObstacleFactory
    {
        private readonly ObstaclePool _obstaclePool;

         public ObstacleFactory(Obstacle[] obstaclePrefabs, int quantityOfEachObjects)
         {
             var parentForPoolObjects = new GameObject("parent_for_pooled_objects").transform;
             _obstaclePool = new ObstaclePool(obstaclePrefabs, quantityOfEachObjects, parentForPoolObjects);
         }

         public void Create(int obstacleIndex, ObstacleSpawnPoint spawnPoint)
         {
             Obstacle obstacle = _obstaclePool.GetObject(obstacleIndex, spawnPoint.transform);
             spawnPoint.SetObstacle(obstacle);
         }

         public void ReturnToObjectPool(Obstacle obstacle)
         {
             _obstaclePool.ReturnToPool(obstacle);
         }
         
    }
}