using System;

namespace Character.StateMachine
{
    [Serializable]
    public class PlayerGameState : GameState
    {
        public override void OnStateFixedUpdate(CharacterController controller)
        {
            controller.Movement.Move();
        }
    }
}

