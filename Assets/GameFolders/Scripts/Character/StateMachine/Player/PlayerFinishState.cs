using System;
using DG.Tweening;
using UnityEngine;

namespace Character.StateMachine
{
    [Serializable]
    public class PlayerFinishState : FinishState
    {
        private bool _moved;

        protected override void OnStateEnter(CharacterController controller)
        {
            //_moved = false;
            //CinemachineController.instance.SetTarget(this) ;
        }

        public override void OnStateFixedUpdate(CharacterController controller)
        {
            //if (_moved) return;
            //controller.Movement.Look();
            //PodiumSequence(controller);
        }

        private void PodiumSequence(CharacterController controller)
        {
            _moved = true;
        }
    }
}

