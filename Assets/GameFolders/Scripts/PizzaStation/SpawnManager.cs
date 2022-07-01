using System.Collections.Generic;
using PoolSystem;
using PizzaSystem;
using UnityEngine;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager instance;

        [SerializeField] private List<string> spawnableObjectTypes;

        private void Awake()
        {
            instance = this;
        }
        
        public PizzaBase SpawnObjectAndSetPosition(int stackCount)
        {
            if (CharacterManager.instance.player.transform.position.z + 25 < LevelManager.instance.level.gameAreas[^1].transform.position.z)
            {
                var newObjectHolder = ObjectPool.instance.GetObject("pizzaBoxHolder", 0).transform;
                newObjectHolder.position = CharacterManager.instance.player.transform.position + new Vector3(0, 0, 25);
                newObjectHolder.SetParent(null);

                var tempBase = newObjectHolder.GetComponent<PizzaBase>();
                PrepareObjectHolder(tempBase, stackCount);
                return tempBase;
            }
            else
            {
                return null;
            }
        }

        private void PrepareObjectHolder(PizzaBase pizzaBase, int stackCount)
        {
            pizzaBase.SetObjects(spawnableObjectTypes[Random.Range(0, spawnableObjectTypes.Count)], 0, stackCount);
        }
    }
}

