using InteractionSystem;
using UnityEngine;

namespace PizzaSystem
{
    public class PizzaHolder : PizzaBase, IBeginInteract
    {
        public bool IsInteractable => true;

        public void OnInteractBegin(IInteractor interactor)
        {
            Debug.Log("Finish");
        }
    }
}

