using System;
using PoolSystem;
using PizzaSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using CharacterController = Character.CharacterController;

public enum StackSide
{
    NoWhere,
    Left,
    Right
}
namespace StackSystem
{
    public class StackController : MonoBehaviour
    {
        public static StackController instance;
        public event Action<Transform, Transform> OnStackAdded;
        public event Action<int,int> OnStackUsed;

        public static StackSide stackSide;
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

        private void Update()
        {
            GettingPizzaBoxes();
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
        public void GettingPizzaBoxes()
        {
            if (PizzaHolderManager.instance.CurrentObject == null) return;
            if (PizzaHolderManager.instance.CurrentObject.IsUsed) return;
            GetPizza();
        }

        private void GetPizza()
        {
            var amount = ObjectAmount();
            if(amount != 0)
            {
                if (stackSide == StackSide.Left)
                    PizzaHolderManager.instance.CurrentObject.ObjectsHolderToPlayer(amount, _leftStackParent);
                else if(stackSide == StackSide.Right)
                    PizzaHolderManager.instance.CurrentObject.ObjectsHolderToPlayer(amount, _rightStackParent);
                if (PizzaHolderManager.instance.CurrentObject.ObjectCount > 0)
                    ObjectMoveToGround(amount);
            }
            OnStackUsed?.Invoke(LeftStackCount,RightStackCount);
            OnStackAdded?.Invoke(_leftStackParent, _rightStackParent);
        }

        private void ObjectMoveToGround(int count)
        {
            for (int i = 0; i < Mathf.Abs(count); i++)
            {
                if (stackSide == StackSide.Left)
                    _leftStackParent.GetChild(i).GetComponent<Pizza>().SetLost();
                else if (stackSide == StackSide.Right)
                    _rightStackParent.GetChild(i).GetComponent<Pizza>().SetLost();
            }
        }
        private int ObjectAmount()
        {
            var givenObj = 0;
            if (PizzaHolderManager.instance.CurrentObject.ObjectCount > 0)
                givenObj = PizzaHolderManager.instance.CurrentObject.ObjectCount;
            else
            {
                if (stackSide == StackSide.Left)
                {
                    if (Mathf.Abs(PizzaHolderManager.instance.CurrentObject.ObjectCount) >= LeftStackCount)
                        givenObj = -LeftStackCount;
                    else
                        givenObj = PizzaHolderManager.instance.CurrentObject.ObjectCount;
                }
                else if(stackSide == StackSide.Right)
                {
                    if (Mathf.Abs(PizzaHolderManager.instance.CurrentObject.ObjectCount) >= RightStackCount)
                        givenObj = -RightStackCount;
                    else
                        givenObj = PizzaHolderManager.instance.CurrentObject.ObjectCount;
                }
            }
            return givenObj;
        }
        public Transform StackSideSelector()
        {
            if (stackSide == StackSide.Left)
            {
                return _leftStackParent;
            }

            return _rightStackParent;
        }

//#if UNITY_EDITOR
//        [SerializeField, BoxGroup("Box", false), HorizontalGroup("Box/Debug", .5f), LabelWidth(48)] private int _amount;
//        [HorizontalGroup("Box/Debug"), Button, DisableInEditorMode, LabelText("Add Stack")]
//        public void Editor_AddStack()
//        {
//            var interactor = GetComponentInParent<CharacterController>();
//            for (var i = 0; i < _amount; i++)
//            {
//                //var obj = PizzaManager.instance.SpawnObject();
//                //obj.OnInteractBegin(interactor);
//            }
//        }
//#endif
    }
}

