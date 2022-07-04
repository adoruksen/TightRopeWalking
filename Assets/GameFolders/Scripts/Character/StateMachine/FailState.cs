using DG.Tweening;
using Managers;

namespace Character.StateMachine
{
    public class FailState : State
    {
        protected override void OnStateEnter(CharacterController controller)
        {
            controller.DOKill();
            controller.Animation.TriggerIdle();
            controller.Rigidbody.isKinematic = true;
            GameManager.instance.FailGameMode();
        }
    }
}


