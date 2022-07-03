using System;
using System.Collections.Generic;
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
        public int LeftStackCount => _leftStickObjectsCount;
        private int _leftStickObjectsCount;
        public int RightStackCount => _rightStickObjectsCount;
        private int _rightStickObjectsCount;

        [SerializeField] private Transform _rightStackParent;
        [SerializeField] private Transform _leftStackParent;


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
                if (stackSide == StackSide.NoWhere) return;
                if (StackParent == _leftStackParent) _leftStickObjectsCount += amount;
                if (StackParent == _rightStackParent) _rightStickObjectsCount += amount;
                PizzaHolderManager.instance.CurrentObject.ObjectsHolderToPlayer(amount, StackParent);

                if (PizzaHolderManager.instance.CurrentObject.ObjectCount < 0)
                    ObjectMoveToGround(amount);
            }
            for (int i = 0; i < amount; i++)
            {
                OnStackAdded?.Invoke(_leftStackParent, _rightStackParent);
            }
            OnStackUsed?.Invoke(LeftStackCount,RightStackCount);
        }

        private void ObjectMoveToGround(int count)
        {
            Transform child = null;
            var removedList = new List<Transform>();
            for (int i = 0; i < Mathf.Abs(count); i++)
            {
                if (stackSide == StackSide.Left)
                {
                    child = _leftStackParent.GetChild(i);
                    child.GetComponent<Pizza>().SetLost();
                }
                else if (stackSide == StackSide.Right)
                {
                    child = _rightStackParent.GetChild(i);
                    child.GetComponent<Pizza>().SetLost();
                }
                removedList.Add(child);
            }
            if (stackSide == StackSide.Left)
            {
                for (int i = 0; i < removedList.Count; i++)
                {
                    removedList[i].SetParent(null);
                }
            }
            else if (stackSide == StackSide.Right)
            {
                for (int i = 0; i < removedList.Count; i++)
                {
                    removedList[i].SetParent(null);
                }
            }
            removedList.Clear();
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
                    if (Mathf.Abs(PizzaHolderManager.instance.CurrentObject.ObjectCount) >= _leftStickObjectsCount)
                        givenObj = -_leftStickObjectsCount;
                    else
                        givenObj = PizzaHolderManager.instance.CurrentObject.ObjectCount;
                }
                else if(stackSide == StackSide.Right)
                {
                    if (Mathf.Abs(PizzaHolderManager.instance.CurrentObject.ObjectCount) >= _rightStickObjectsCount)
                        givenObj = -_rightStickObjectsCount;
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

