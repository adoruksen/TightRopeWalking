using UnityEngine;
using Managers.GameModes;
using System;

namespace Managers
{
    public class GameManager : BaseGameManager
    {
        public static event GameEvents OnGameInitialized;
        public static event GameEvents OnGameEnd;

        public static GameManager instance;

        [SerializeField] private GameMode _defaultGameMode;

        private GameMode _currentGameMode;

        public bool IsPlaying { get; private set; }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            InitializeGameMode(_defaultGameMode);
        }

        public void InitializeGameMode(GameMode gameMode)
        {
            if (_currentGameMode != null) _currentGameMode.DeinitializeGameMode();
            _currentGameMode = gameMode;
            _currentGameMode.InitializeGameMode();
            LevelInitialize();
        }

        public void StartGameMode()
        {
            _currentGameMode.StartGameMode(LevelStart);
            IsPlaying = true;
        }

        protected void LevelInitialize()
        {
            OnGameInitialized?.Invoke();
        }

        protected void LevelEnd()
        {
            OnGameEnd?.Invoke();
        }
    }
}

