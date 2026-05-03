using UnityEngine;
using UnityEngine.SceneManagement;

namespace DragonSeal.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public enum GameState { MainMenu, Playing, DayEnd, GameOver, Ending }
        private GameState _currentState;
        public GameState CurrentState
        {
            get => _currentState;
            private set
            {
                _currentState = value;
                Debug.Log($"GameState changed to: {_currentState}");
            }
        }
    }
}