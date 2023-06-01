using UnityEngine;

namespace Models.CarModule
{
    public class CarMovingData
    {
        public Vector3 Position { get; }

        public Quaternion Rotation { get; }
        
        public bool IsGrounded { get; }

        public CarMovingData(bool isGrounded)
        {
            IsGrounded = isGrounded;
            Position = Vector3.zero;
            Rotation = Quaternion.identity;
        }
        
        public CarMovingData(Vector3 position, Quaternion rotation, bool isGrounded)
        {
            Position = position;
            Rotation = rotation;
            IsGrounded = isGrounded;
        }

        public CarMovingData Copy()
        {
            return new CarMovingData(Position, Rotation, IsGrounded);
        }
    }
}