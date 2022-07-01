using System;
using PoolSystem;
using PizzaSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using CharacterController = Character.CharacterController;

public enum StackSide
{
    Left,
    Right
}
namespace StackSystem
{
    public class StackController : MonoBehaviour
    {
        public static StackController instance;
        //public event Action<Pizza,Transform> OnStackAdded;
        //public event Action OnStackUsed;

        public static StackSide stackSideTransform;
        public int LeftStackCount => _leftStackParent.childCount;
        [SerializeField] private Transform _leftStackParent;
        public int RightStackCount => _rightStackParent.childCount;
        [SerializeField] private Transform _rightStackParent;

        public Transform StackParent => StackSideSelector();

        [ShowInInspector,ReadOnly,PropertyOrder(-1)] public int Stack { get; private set; }

        private void Awake()
        {
            instance = this;
        }

        //public void AddStack(Pizza obj,Transform parent)
        //{
        //    Stack++;
        //    OnStackAdded?.Invoke(obj,parent);
        //}

        //public void UseStack()
        //{
        //    Stack--;
        //    OnStackUsed?.Invoke();
        //}

        public Transform StackSideSelector()
        {
            if (stackSideTransform == StackSide.Left)
            {
                return _leftStackParent;
            }

            return _rightStackParent;
        }

#if UNITY_EDITOR
        [SerializeField, BoxGroup("Box", false), HorizontalGroup("Box/Debug", .5f), LabelWidth(48)] private int _amount;
        [HorizontalGroup("Box/Debug"), Button, DisableInEditorMode, LabelText("Add Stack")]
        public void Editor_AddStack()
        {
            var interactor = GetComponentInParent<CharacterController>();
            for (var i = 0; i < _amount; i++)
            {
                //var obj = PizzaManager.instance.SpawnObject();
                //obj.OnInteractBegin(interactor);
            }
        }
#endif
    }
}

