using System;
using LevelSystem;
using UnityEngine;

namespace Managers.GameModes
{
    [CreateAssetMenu(menuName = "Game/GameMode/DefaultGameMode", fileName = "DefaultGameMode", order = -399)]
    public class DefaultGameMode : GameMode
    {
        public LevelConfig[] Levels;

        public override void InitializeGameMode()
        {
            var config = Levels[0]; //tek level oldugu icin
            LevelManager.instance.SpawnLevel(config.parts);
        }
        public override void CompleteGameMode()
        {
            throw new NotImplementedException();
        }

        public override void DeinitializeGameMode()
        {
            throw new NotImplementedException();
        }

        public override void FailGameMode()
        {
            throw new NotImplementedException();
        }

        

        public override void SkipGameMode()
        {
            throw new NotImplementedException();
        }

        public override void StartGameMode(Action levelStart)
        {
            throw new NotImplementedException();
        }
    }
}

