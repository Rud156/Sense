using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Scene.Main
{
    public class GameMainController : MonoBehaviour
    {
        public PauseAndResume pauseAndResume;
        public SingleFader sceneFader;

        private bool _pauseMenuOpen;
        private bool _isGameOver;

        #region Unity Functions

        private void Start()
        {
            sceneFader.OnFadeOutComplete += SwitchSceneOnFadeOut;
            sceneFader.StartFadeIn(true);
        }

        private void Update()
        {
            if (!Input.GetButtonDown(ControlConstants.CloseButton))
            {
                return;
            }

            if (_pauseMenuOpen)
            {
                _pauseMenuOpen = false;
                pauseAndResume.ResumeGame();
            }
            else
            {
                _pauseMenuOpen = true;
                pauseAndResume.PauseGame();
            }
        }

        #endregion

        #region External Functions

        public void QuitToMenu()
        {
            sceneFader.StartFadeOut(true);
            _isGameOver = false;
        }

        public void SwitchToGameOverScene()
        {
            sceneFader.StartFadeOut(true);
            _isGameOver = true;
        }

        #endregion

        #region Utility Functions

        private void SwitchSceneOnFadeOut()
        {
            sceneFader.OnFadeOutComplete -= SwitchSceneOnFadeOut;
            if (_isGameOver)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }

        #endregion

        #region Singleton

        private static GameMainController _instance;
        public static GameMainController Instance => _instance;

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

        #endregion
    }
}