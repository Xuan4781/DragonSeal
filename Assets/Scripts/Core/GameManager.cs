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

        public enum EndingType { None, StayedSilent, ExposedTruth }

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

        private EndingType _ending = EndingType.None;
        public EndingType CurrentEnding
        {
            get => _ending;
            set => _ending = value;
        }

        private const int SCENE_MAINMENU = 0;
        private const int SCENE_GAME = 1;
        private const int SCENE_DAYEND = 2;
        private const int SCENE_GAMEOVER = 3;
        private const int SCENE_ENDING = 4;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            CurrentState = GameState.MainMenu;
        }

        public void StartNewGame()
        {
            _dayNumber = 1;
            _trustRating = 100;
            _ending = EndingType.None;
            CurrentState = GameState.Playing;
            SceneManager.LoadScene(SCENE_GAME);
        }

        public void EndDay()
        {
            CurrentState = GameState.DayEnd;
            SceneManager.LoadScene(SCENE_DAYEND);
        }

        public void StartNextDay()
        {
            DayNumber++;
            if (DayNumber > 3)
            {
                CurrentState = GameState.Ending;
                SceneManager.LoadScene(SCENE_ENDING);
            }
            else
            {
                CurrentState = GameState.Playing;
                SceneManager.LoadScene(SCENE_GAME);
            }
        }

        public void TriggerGameOver()
        {
            CurrentState = GameState.GameOver;
            SceneManager.LoadScene(SCENE_GAMEOVER);
        }

        public void RestartGame()
        {
            StartNewGame();
        }

        public void GoToMainMenu()
        {
            CurrentState = GameState.MainMenu;
            SceneManager.LoadScene(SCENE_MAINMENU);
        }

        public void GoToEnding()
        {
            CurrentState = GameState.Ending;
            SceneManager.LoadScene(SCENE_ENDING);
        }

        public void ModifyTrust(int amount)
        {
            TrustRating += amount;
            Debug.Log($"Trust: {TrustRating}");

            if (TrustRating <= 0)
                TriggerGameOver();
        }
    }
}