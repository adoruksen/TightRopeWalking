using System;
using InteractionSystem;
using StackSystem;
using Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;
using CharacterController = Character.CharacterController;

public class Pizza : MonoBehaviour, IBeginInteract
{
    public event Action OnCollected;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;
    private float _verticalForce = 300;
    private float _horizontalForce = 150;
    [ShowInInspector, ReadOnly] public bool IsInteractable { get; private set; }

    public void OnInteractBegin(IInteractor interactor)
    {
        var controller = (CharacterController)interactor;
        Collect(controller);
    }

    private void Collect(CharacterController controller)
    {
        OnCollected?.Invoke();
        controller.StackController.AddStack(this,controller.StackController.StackParent);
        SetInteractable(false);
    }

    public void SetLost()
    {
        transform.SetParent(GameManager.instance.defaultParent);
        SetInteractable(true);
        FlingPizza();
    }

    public void SetInteractable(bool interactable)
    {
        _collider.enabled = interactable;
        _rigidbody.isKinematic = !interactable;
        IsInteractable = interactable;
        //PizzaManager.instance.SetObjectAvailable(this, interactable);
    }

    private void FlingPizza()
    {
        Vector3 force = Random.insideUnitCircle * _horizontalForce;
        force = new Vector3(force.x, Random.value * _verticalForce, force.y);
        _rigidbody.AddForce(force);
    }

    
}
