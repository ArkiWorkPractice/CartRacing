using System;
using Obstacles.Abstract;
using ObstacleSpawn;
using ServiceLocatorModule.Interfaces;
using UnityEngine;

namespace Factory
{
    public class ObstacleFactory
    {
        private readonly int _quantityOfEachObjects;
         private readonly ObjectPool.ObjectPool _objectPool;

         public ObstacleFactory(Obstacle[] obstaclePrefabs, int quantityOfEachObjects)
         {
             var parentForPoolObjects = new GameObject("parent_for_pooled_objects").transform;
             _objectPool = new ObjectPool.ObjectPool(obstaclePrefabs, _quantityOfEachObjects, parentForPoolObjects);
         }

         public void Create(int obstacleIndex, ObstacleSpawnPoint spawnPoint)
         {
             Obstacle obstacle = _objectPool.GetObject(obstacleIndex, spawnPoint.transform);
             spawnPoint.SetObstacle(obstacle);
         }

         public void ReturnToObjectPool(Obstacle obstacle)
         {
             _objectPool.ReturnToPool(obstacle);
         }
         
    }
}