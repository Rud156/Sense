using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene.Main
{
    public class PauseAndResume : MonoBehaviour
    {
        public GameObject pauseMenu;

        public delegate void PauseEnabled();

        public delegate void PauseDisabled();

        public PauseEnabled OnPauseEnabled;
        public PauseDisabled OnPauseDisabled;

        private void Start() => pauseMenu.SetActive(false);

        public void PauseGame()
        {
            OnPauseEnabled?.Invoke();

            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;

            OnPauseDisabled?.Invoke();
        }

        public void QuitToMenu()
        {
            Time.timeScale = 1;
            GameMainController.Instance.QuitToMenu();
        }

        #region Singleton

        private static PauseAndResume _instance;
        public static PauseAndResume Instance => _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }

            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        #endregion Singleton
    }
}