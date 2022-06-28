using UnityEngine;
using Managers.GameModes;
using System;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public event Action OnGameStart;
        public event Action OnGameEnd;

        [SerializeField] private GameMode _defaultGameMode;

        private GameMode _currentGameMode;


        public bool IsPlaying { get; private set; }

        private void Start()
        {
            InitializeGameMode(_defaultGameMode);
        }

        private void InitializeGameMode(GameMode gameMode)
        {
            if (_currentGameMode != null) _currentGameMode.DeinitializeGameMode();
            _currentGameMode = gameMode;
            _currentGameMode.InitializeGameMode();
            OnGameStart?.Invoke();
        }
    }
}

