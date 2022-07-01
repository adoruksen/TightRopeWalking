using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PizzaSystem
{
    public class PizzaHolderManager : MonoBehaviour
    {
        public static PizzaHolderManager instance;


        [SerializeField] private PizzaBase _currentObject;
        public PizzaBase CurrentObject => _currentObject;

        public static event Action OnMoveFinish;

        private void Awake()
        {
            instance = this;
        }

        public void SpawnNewObjectHolder()
        {
            var randomHolderCount = 0;
            if oyun basi degilse
            if ()
            {

            }
            //oyunbasiysa
            else
            {
                randomHolderCount = Mathf.
            }
        }
        public void MoveFinisher()
        {

        }
    }
}

