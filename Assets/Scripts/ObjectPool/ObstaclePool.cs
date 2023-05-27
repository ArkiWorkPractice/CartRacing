using System;
using System.Collections.Generic;
using Obstacles.Abstract;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ObjectPool
{
    public class ObstaclePool : MonoBehaviour
    {
         private readonly Transform _parentForPoolObjects;
         private readonly Obstacle[] _obstaclePrefabs;
         private readonly int _quantityOfEachObjects;

        private readonly Dictionary<Type, Queue<Obstacle>> _objectPool;

        public ObstaclePool(Obstacle[] obstacles, int quantityObjects, Transform parentObject)
        {
            _objectPool = new Dictionary<Type, Queue<Obstacle>>();
            _obstaclePrefabs = obstacles;
            _quantityOfEachObjects = quantityObjects;
            _parentForPoolObjects = parentObject;
            InstantiateObjectPool();
        }

        private void InstantiateObjectPool()
        {
            for (int i = 0; i < _obstaclePrefabs.Length; i++)
            {
                Queue<Obstacle> currObstacleQueue = new Queue<Obstacle>();
                for (int j = 0; j < _quantityOfEachObjects; j++)
                {
                    currObstacleQueue.Enqueue(CreateObject(i));
                }
                _objectPool.Add(_obstaclePrefabs[i].GetType(),currObstacleQueue);
            }
        }

        private Obstacle CreateObject(int obstacleIndex)
        {
            Obstacle obstacle = Instantiate(_obstaclePrefabs[obstacleIndex], _parentForPoolObjects);
            obstacle.gameObject.SetActive(false);

            return obstacle;
        }

        public Obstacle GetObject(int obstacleIndex, Transform parent)
        {
            Obstacle gotObstacle = _objectPool[_obstaclePrefabs[obstacleIndex].GetType()].Count != 0 ? 
                _objectPool[_obstaclePrefabs[obstacleIndex].GetType()].Dequeue() : 
                CreateObject(obstacleIndex);
            var obstacleTransform = gotObstacle.transform;
            obstacleTransform.position = parent.position;
            var rotation = obstacleTransform.rotation;
            obstacleTransform.rotation = Quaternion.Euler(rotation.x, Random.Range(0f, 180f), rotation.z);
            obstacleTransform.parent = parent;
            gotObstacle.gameObject.SetActive(true);
            return gotObstacle;
        
        }

        public void ReturnToPool(Obstacle obstacle)
        {
            _objectPool[obstacle.GetType()].Enqueue(obstacle);
            obstacle.gameObject.SetActive(false);
            obstacle.transform.parent = _parentForPoolObjects;
        }
    }
}
