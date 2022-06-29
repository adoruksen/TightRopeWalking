using Sirenix.OdinInspector;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace StackSystem
{
    public class StackVisualController : MonoBehaviour
    {
        private StackController _stackController;

        [ShowInInspector] private readonly Stack<Pizza> _stackedObjects = new Stack<Pizza>();

        [SerializeField] private float _distance;

        private void Awake()
        {
            _stackController = GetComponent<StackController>();
        }


        private void OnEnable()
        {
            _stackController.OnStackAdded += UpdateVisualAdded;
            _stackController.OnStackUsed += UpdateVisualUsed;
        }

        private void OnDisable()
        {
            _stackController.OnStackAdded -= UpdateVisualAdded;
            _stackController.OnStackUsed -= UpdateVisualUsed;
        }

        private void UpdateVisualAdded(Pizza obj,Transform parent)
        {
            _stackedObjects.Push(obj);
            var objTransform = obj.transform;
            objTransform.SetParent(parent);
            objTransform.localPosition = Vector3.up * (_stackController.Stack * _distance);
            objTransform.localRotation = Quaternion.identity;
        }

        private void UpdateVisualUsed()
        {
            var obj = _stackedObjects.Pop();
            obj.transform.DOScale(Vector3.zero, .2f).OnComplete(() =>
            {
                //PizzaManager.instance.RemoveObject(obj);
                obj.transform.localScale = Vector3.one;
            });
        }
    }
}

