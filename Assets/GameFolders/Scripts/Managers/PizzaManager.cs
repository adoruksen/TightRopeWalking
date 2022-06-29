using Managers;
using UnityEngine;

namespace StackSystem
{
    public class PizzaManager : PooledObjectManager<Pizza>
    {
        public static PizzaManager instance;

        protected override void Awake()
        {
            instance = this;
            base.Awake();
        }

        protected override void OnObjectSpawned(Pizza obj)
        {
            obj.SetInteractable(true);
        }
    }
}

