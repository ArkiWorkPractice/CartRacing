using Models.CarModule;
using UnityEngine;

namespace CarModule.CarControl
{
    [CreateAssetMenu(fileName = "CarConfig", menuName = "CarModule/CarConfig", order = 1)]
    public class CarConfigSo : ScriptableObject
    {
        // car config
        [SerializeField] private CarConfig carConfig;

        public CarConfig GetConfig()
        {
            return carConfig.Copy();
        }
    }
}