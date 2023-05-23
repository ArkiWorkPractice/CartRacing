using UnityEngine;

namespace CarModule
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform carToFollow;
        [SerializeField] private Vector2 offset;

        public void LateUpdate()
        {
            //transform.LookAt(carToFollow);
            Vector3 localOffset = carToFollow.transform.up * offset.x - carToFollow.forward * offset.y;
            transform.position = carToFollow.position + localOffset;
            transform.rotation = new Quaternion(carToFollow.rotation.x, carToFollow.rotation.y, 0, carToFollow.rotation.w);
        }
    }
}