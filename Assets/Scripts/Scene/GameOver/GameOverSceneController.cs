using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Scene.GameOver
{
    public class GameOverSceneController : MonoBehaviour
    {
        public SingleFader sceneFader;
        public float sceneSwitchDelay;
        public PostProcessVolume postProcessVolume;
        public float glitchSwitchTime;

        [Header("Player State Display")] public TextTyper sceneTyper;
        [TextArea] public string successText;
        [TextArea] public string failText;

        private bool _sceneFadeComplete;

        private float _currentSwitchTime;
        private ColorGrading _colorGrading;
        private Grain _cameraGrain;

        #region Unity Functions

        private void Start()
        {
            sceneFader.OnFadeInComplete += HandleSceneFadeIn;
            sceneFader.OnFadeOutComplete += HandleSceneFadeOut;
            sceneTyper.OnTypingCompleted += HandleTextTypingComplete;

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

        #region Utility Functions

        private void SwitchToRandomGlitchEffect()
        {
            float saturationValue = Random.Range(-100, 0);
            float grainIntensity = Random.value;

            _colorGrading.saturation.value = saturationValue;
            _cameraGrain.intensity.value = grainIntensity;
        }

        private void HandleSceneFadeIn()
        {
            sceneFader.OnFadeInComplete -= HandleSceneFadeIn;

            if (GameOverSceneData.PlayerCollectedAllAbilities)
            {
                sceneTyper.UpdateText(successText);
            }
            else
            {
                sceneTyper.UpdateText(failText);
            }

            sceneTyper.StartTyping();
        }

        private void HandleSceneFadeOut()
        {
            sceneFader.OnFadeOutComplete -= HandleSceneFadeOut;
            SceneManager.LoadScene(0);
        }

        private void HandleTextTypingComplete()
        {
            sceneTyper.OnTypingCompleted -= HandleTextTypingComplete;

            StartCoroutine(ActivateFadeOut());
        }

        private IEnumerator ActivateFadeOut()
        {
            yield return new WaitForSeconds(sceneSwitchDelay);
            sceneFader.StartFadeOut();
        }

        #endregion
    }
}