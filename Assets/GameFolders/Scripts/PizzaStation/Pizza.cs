using System;
using InteractionSystem;
using PoolSystem;
using StackSystem;
using Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;
using CharacterController = Character.CharacterController;

namespace PizzaSystem
{
    public class Pizza : MonoBehaviour, IPooled
    {
        public event Action OnCollected;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        private float _verticalForce = 300;
        private float _horizontalForce = 150;
        [ShowInInspector, ReadOnly] public bool IsInteractable { get; private set; }
        public string PoolType { get; set; }
        public int PoolId { get; set; }

        public void SetLost()
        {
            SetInteractable(true);
            FlingPizza();
        }

        public void SetInteractable(bool interactable)
        {
            _collider.enabled = interactable;
            _rigidbody.isKinematic = !interactable;
            _rigidbody.useGravity = interactable;
            IsInteractable = interactable;
            //PizzaManager.instance.SetObjectAvailable(this, interactable);
        }

        private void FlingPizza()
        {
            Vector3 force = Random.insideUnitCircle * _horizontalForce;
            force = new Vector3(force.x, Random.value * _verticalForce, force.y);
            _rigidbody.AddForce(force);
            DOVirtual.DelayedCall(2f, () => { ReturnToPool(); });
        }

        private void ReturnToPool()
        {
            ObjectPool.instance.PutObject(PoolType, PoolId, gameObject);
        }
    }
}

