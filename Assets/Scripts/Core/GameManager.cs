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

        // track day
        private int _dayNumber = 1;
        public int DayNumber
        {
            get => _dayNumber;
            private set => _dayNumber = Mathf.Clamp(value, 1, 3);
        }

        // trust rating
        private int _trustRating = 100;
        public int TrustRating
        {
            get => _trustRating;
            private set => _trustRating = Mathf.Clamp(value, 0, 100);
        }

    }
}