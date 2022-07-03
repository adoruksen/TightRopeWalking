using Sirenix.OdinInspector;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using PizzaSystem;
using TMPro;
using CharacterController = Character.CharacterController;

namespace StackSystem
{
    public class StackVisualController : MonoBehaviour
    {
        private StackController _stackController;
        private CharacterController _controller;

        [SerializeField] private TMP_Text _leftHolderText;
        [SerializeField] private TMP_Text _rightHolderText;

        private float _balanceValue=0;

        private void Awake()
        {
            _stackController = GetComponent<StackController>();
            _controller = GetComponent<CharacterController>();
        }


        private void OnEnable()
        {
            _stackController.OnStackUsed += UpdateVisualUsed;
            _stackController.OnStackUsed += StacksBalanceSystem;
            _stackController.OnStackAdded += UpdateStackPosition;
        }

        private void OnDisable()
        {
            _stackController.OnStackUsed -= UpdateVisualUsed;
            _stackController.OnStackUsed -= StacksBalanceSystem;
            _stackController.OnStackAdded -= UpdateStackPosition;

        }

        private void UpdateVisualUsed(int leftHolderCount,int rightHolderCount)
        {
            _leftHolderText.text = $"{leftHolderCount}";
            _rightHolderText.text = $"{rightHolderCount}";
            StackController.stackSide = StackSide.NoWhere;
        }

        private void UpdateStackPosition(Transform leftParent, Transform rightParent)
        {
            for (int i = 0; i < leftParent.childCount; i++)
            {
                leftParent.GetChild(i).DOLocalMoveY(.05f * i, .2f);
            }
            for (int i = 0; i < rightParent.childCount; i++)
            {
                rightParent.GetChild(i).DOLocalMoveY(.05f * i, .2f);
            }
        }

        private void StacksBalanceSystem(int leftHolderCount,int rightHolderCount)
        {
            if(leftHolderCount == rightHolderCount)
            {
                _balanceValue = Mathf.MoveTowards(_balanceValue, 0, .1f);
                _controller.Animation.TriggerMove();
            }
            else
            {
                if(leftHolderCount > rightHolderCount)
                {
                    _balanceValue = Mathf.MoveTowards(_balanceValue,-1,.6f*((float)(leftHolderCount-rightHolderCount))/100);
                    _controller.Animation.TriggerLeftSide();
                }
                if(rightHolderCount > leftHolderCount)
                {
                    _balanceValue = Mathf.MoveTowards(_balanceValue, 1, .6f * ((float)(rightHolderCount - leftHolderCount)) / 100);
                    _controller.Animation.TriggerRightSide();
                }
            }
            _controller.transform.localEulerAngles = new Vector3(0, 0, -(_balanceValue * 40));
        }
    }
}

