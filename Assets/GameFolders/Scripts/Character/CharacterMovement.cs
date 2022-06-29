using UnityEngine;

namespace Character
{
    public class CharacterMovement : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        public float MoveSpeed;
        public bool IsActive;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        public void Move()
        {
            if (!IsActive) return;
            var movement = Vector3.forward * MoveSpeed;
            _rigidbody.velocity = movement;
        }

        public void Look()
        {
            if (!IsActive) return;

            var rotation = Quaternion.Lerp(_rigidbody.rotation, Quaternion.LookRotation(Vector3.back), .2f);
            _rigidbody.MoveRotation(rotation);
        }
    }
}

