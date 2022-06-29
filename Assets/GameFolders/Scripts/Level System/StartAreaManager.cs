using Managers;

namespace LevelSystem
{
    public class StartAreaManager : GameAreaManager
    {
        public override void OnCharacterEntered(Character.CharacterController controller)
        {
            if (!GameManager.instance.IsPlaying) return;
            controller.SetState(controller.GameState);
        }
    }
}

