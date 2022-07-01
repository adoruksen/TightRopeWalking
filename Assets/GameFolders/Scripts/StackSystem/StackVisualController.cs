using Sirenix.OdinInspector;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using PizzaSystem;
using TMPro;

namespace StackSystem
{
    public class StackVisualController : MonoBehaviour
    {
        private StackController _stackController;

        [SerializeField] private TMP_Text _leftHolderText;
        [SerializeField] private TMP_Text _rightHolderText;

        private void Awake()
        {
            _stackController = GetComponent<StackController>();
        }


        private void OnEnable()
        {
            _stackController.OnStackUsed += UpdateVisualUsed;
            _stackController.OnStackAdded += UpdateStackPosition;
        }

        private void OnDisable()
        {
            _stackController.OnStackUsed -= UpdateVisualUsed;
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
    }
}

