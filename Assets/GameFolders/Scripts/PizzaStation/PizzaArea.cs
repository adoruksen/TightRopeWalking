using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PizzaSystem
{
    public class PizzaArea : MonoBehaviour
    {
        [SerializeField] test test;
        [SerializeField] private GameObject _pizza;
        

        private void InitializePizza()
        {
            var pizza = Instantiate(_pizza, transform);
        }
    }
}

