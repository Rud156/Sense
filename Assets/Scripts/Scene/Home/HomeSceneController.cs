using Scene.GameOver;
using UI;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Scene.Home
{
    public class HomeSceneController : MonoBehaviour
    {
        public SingleFader sceneFader;
        public PostProcessVolume postProcessVolume;
        public float glitchSwitchTime;

        private float _currentSwitchTime;
        private ColorGrading _colorGrading;
        private Grain _cameraGrain;

        #region Unity Functions

        private void Start()
        {
            sceneFader.OnFadeOutComplete += HandleSceneFadeOut;
            sceneFader.StartFadeIn(true);

            _colorGrading = postProcessVolume.profile.GetSetting<ColorGrading>();
            _cameraGrain = postProcessVolume.profile.GetSetting<Grain>();

            _currentSwitchTime = glitchSwitchTime;
        }

        private void Update()
        {
            _currentSwitchTime -= Time.deltaTime;
            if (_currentSwitchTime <= 0)
            {
                SwitchToRandomGlitchEffect();
                _currentSwitchTime = glitchSwitchTime;
            }
        }

        #endregion

        #region External Functions

        public void StartGame() => sceneFader.StartFadeOut();

        public void QuitGame() => Application.Quit();

        #endregion

        #region Utility Functions

        private void SwitchToRandomGlitchEffect()
        {
            float saturationValue = Random.Range(-100, 0);
            float grainIntensity = Random.value;

            _colorGrading.saturation.value = saturationValue;
            _cameraGrain.intensity.value = grainIntensity;
        }

        private void HandleSceneFadeOut()
        {
            sceneFader.OnFadeOutComplete -= HandleSceneFadeOut;

            GameOverSceneData.PlayerCollectedAllAbilities = false;
            SceneManager.LoadScene(1);
        }

        #endregion
    }
}