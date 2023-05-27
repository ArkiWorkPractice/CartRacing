using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ObstacleSpawnerConfig", menuName = "ScriptableObjects/ObstacleSpawnerConfig",order = 2)]
    public class ObstacleSpawnerConfig : ScriptableObject
    {
        [SerializeField] private int quantityForEachObject;

        public int QuantityForEachObject => quantityForEachObject;
    }
}