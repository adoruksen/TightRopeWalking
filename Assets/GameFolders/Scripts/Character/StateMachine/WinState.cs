using DG.Tweening;

namespace Character.StateMachine
{
    public class WinState : State
    {
        protected override void OnStateEnter(CharacterController controller)
        {
            controller.DOKill();
            controller.Rigidbody.isKinematic = true;
        }
    }
}
