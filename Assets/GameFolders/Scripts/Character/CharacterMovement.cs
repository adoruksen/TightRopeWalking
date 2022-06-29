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
    }
}

