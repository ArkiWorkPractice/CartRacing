using System;
using System.Collections.Generic;
using Obstacles.Abstract;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ObjectPool
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private Transform parentForPoolObjects;
        [SerializeField] private Obstacle[] obstaclePrefabs;
        [SerializeField] private int quantityOfEachObjects;

        private readonly Dictionary<Type, Queue<Obstacle>> _objectPool;

        public ObjectPool(Obstacle[] obstacles, int quantityObjects, Transform parentObject)
        {
            _objectPool = new Dictionary<Type, Queue<Obstacle>>();
            obstaclePrefabs = obstacles;
            quantityOfEachObjects = quantityObjects;
            parentForPoolObjects = parentObject;
            InstantiateObjectPool();
        }

        private void InstantiateObjectPool()
        {
            for (int i = 0; i < obstaclePrefabs.Length; i++)
            {
                Queue<Obstacle> currObstacleQueue = new Queue<Obstacle>();
                for (int j = 0; j < quantityOfEachObjects; j++)
                {
                    currObstacleQueue.Enqueue(CreateObject(i));
                }
                _objectPool.Add(obstaclePrefabs[i].GetType(),currObstacleQueue);
            }
        }

        private Obstacle CreateObject(int obstacleIndex)
        {
            Obstacle obstacle = Instantiate(obstaclePrefabs[obstacleIndex], parentForPoolObjects);
            obstacle.gameObject.SetActive(false);

            return obstacle;
        }

        public Obstacle GetObject(int obstacleIndex, Transform parent)
        {
            Obstacle gotObstacle = _objectPool[obstaclePrefabs[obstacleIndex].GetType()].Count != 0 ? 
                _objectPool[obstaclePrefabs[obstacleIndex].GetType()].Dequeue() : 
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
            obstacle.transform.parent = parentForPoolObjects;
        }
    }
}
