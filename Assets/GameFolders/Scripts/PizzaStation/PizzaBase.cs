using PoolSystem;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace PizzaSystem
{
    public abstract class PizzaBase : MonoBehaviour, IPooled
    {
        [SerializeField] private TMP_Text objectCountText;
        [SerializeField] private List<Transform> objects;

        public int ObjectCount { get => objectCount; private set { objectCount = value; } }
        [SerializeField] private int objectCount;
        public bool IsUsed { get => isUsed; private set { isUsed = value; } }
        [SerializeField] private bool isUsed;

        private int _moveToPlayerIndex;


        public string PoolType { get; set; }
        public int PoolId { get; set; }
        public void SetObjects(string objectPoolType, int objectPoolId, int stackCount)
        {
            objects = new List<Transform>();

            if (stackCount > 0)
            {
                for (int i = 0; i < stackCount; i++)
                {
                    var temp = ObjectPool.instance.GetObject(objectPoolType, objectPoolId).transform;
                    temp.localEulerAngles = Vector3.zero;
                    temp.GetComponent<Pizza>().SetInteractable(false);
                    temp.SetParent(transform);
                    temp.localPosition = new Vector3(0, .5f+(.1f * i), 0);
                    objects.Add(temp);
                }
                objects.Reverse();
            }
            else
            {
                SetText(stackCount);
            }
            IsUsed = false;
            ObjectCount = stackCount;
        }

        public void ObjectsHolderToPlayer(int count, Transform target)
        {
            if (ObjectCount > 0)
            {
                _moveToPlayerIndex = objects.Count - 1;
                MoveToPlayerTarget(target);

                IsUsed = true;
                ObjectCount -= count;
            }
            else
            {
                DOVirtual.Int(ObjectCount, ObjectCount - count, .2f, x =>
                {
                    objectCount = x;
                    SetText(x);

                    if (ObjectCount == 0 && !IsUsed)
                    {
                        IsUsed = true;
                        CreateNewHolderAndReturnPool();
                    }
                });
            }
        }

        private void MoveToPlayerTarget(Transform target)
        {
            DOVirtual.DelayedCall(.2f, () =>
            {
                if (_moveToPlayerIndex > -1)
                {
                    var temp = objects[_moveToPlayerIndex];
                    SetObject(temp, target);

                    objects.RemoveAt(_moveToPlayerIndex);

                    _moveToPlayerIndex--;

                    SetText(objects.Count);
                    MoveToPlayerTarget(target);
                }
                else
                {
                    if (ObjectCount == 0)
                    {
                        CreateNewHolderAndReturnPool();
                    }
                }
            });
        }

        private void SetObject(Transform temp, Transform target)
        {
            if (DOTween.IsTweening(temp))
            {
                DOTween.Kill(temp);
            }

            temp.SetParent(target);
            temp.localEulerAngles = new Vector3(0, 180, 0);
            temp.localScale = Vector3.one * 0.5f;
            temp.DOLocalMove(Vector3.zero, .2f);
        }

        private void CreateNewHolderAndReturnPool()
        {
            ObjectPool.instance.PutObject(PoolType, PoolId, gameObject);
            PizzaHolderManager.instance.SpawnNewObjectHolder();
        }

        private void SetText(int amount)
        {
            if (amount > 0)
            {
                objectCountText.text = $"+{amount}";
                objectCountText.DOColor(Color.green, .1f);
            }
            else
            {
                objectCountText.text = $"{amount}";
                objectCountText.DOColor(Color.red, .1f);
            }
        }
    }
}

