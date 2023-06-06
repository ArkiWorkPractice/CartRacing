using CarModule;
using Levels;
using Obstacles.Abstract;
using UI;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PrefabsContainer", menuName = "ScriptableObjects/PrefabsContainer",order = 1)]
    public class PrefabsContainerSO : ScriptableObject
    {
        public Obstacle[] Obstacles => obstacles;

        public Level[] Levels => levels;
        
        public UIController UIPrefab => uiController;
        public Car CarPrefab => carPrefab;

        [SerializeField] private Obstacle[] obstacles;
        [SerializeField] private Level[] levels;
        [SerializeField] private UIController uiController;
        [SerializeField] private Car carPrefab;
    }
}