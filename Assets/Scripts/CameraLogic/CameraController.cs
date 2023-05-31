using CarModule.CarControl;
using ServiceLocatorModule;
using ServiceLocatorModule.Interfaces;
using UnityEngine;

namespace CameraLogic
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour, IService
    {
        [SerializeField] private CarCameraFollower cameraFollower;

        private void Start()
        {
            ServiceLocator.Instance.RegisterService(this);
        }

        public void Follow(Transform target)
        {
            cameraFollower.Follow(target);
        }
    }
}