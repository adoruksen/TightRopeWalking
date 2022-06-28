using InteractionSystem;
using UnityEngine;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        public Rigidbody Rigidbody { get; private set; }
        public Interactor Interactor { get; private set; }
        public CharacterMovement Movement { get; private set; }
        public CharacterAnimationController Animation { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Movement = GetComponent<CharacterMovement>();
            Interactor = GetComponentInChildren<Interactor>();
        }
    }
}

