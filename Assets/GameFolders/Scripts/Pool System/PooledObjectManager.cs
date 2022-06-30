using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class PooledObjectManager<T> : MonoBehaviour where T:MonoBehaviour
    {
        [SerializeField] private T obj;
        public Transform poolParent;

        [ShowInInspector, ReadOnly] private List<T> _availableObjects;

        [ShowInInspector, ReadOnly] private ObjectPool<T> _pool;

        protected virtual void Awake()
        {
            _availableObjects = new List<T>();
            _pool = new ObjectPool<T>(obj);
        }

        protected virtual void OnObjectSpawned(T obj) { }

        public T SpawnObject()
        {
            var obj = _pool.Get();
            obj.transform.SetParent(GameManager.instance.defaultParent);
            obj.gameObject.SetActive(true);
            OnObjectSpawned(obj);

            return obj;
        }
    }
}

