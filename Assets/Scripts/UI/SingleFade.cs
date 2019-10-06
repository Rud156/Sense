using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class SingleFade : MonoBehaviour
    {
        [Header("Fade Rate")] public float fadeInRate;
        public float fadeOutRate;

        public delegate void FadeInComplete();
        public delegate void FadeOutComplete();

        public FadeInComplete OnFadeInComplete;
        public FadeOutComplete OnFadeOutComplete;

        private Image _fadeImage;
        private bool _activateFadeIn;
        private bool _activateFadeOut;
        private float _currentAlpha;

        private bool _initialized;

        #region Unity Functions

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if (_activateFadeIn)
                FadeIn();
            else if (_activateFadeOut)
                FadeOut();
        }

        #endregion

        #region Utility Functions

        private void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;
            _fadeImage = GetComponent<Image>();
            _currentAlpha = ExtensionFunctions.Map(_fadeImage.color.a, 0, 1, 0, 255);
        }

        public void StartFadeIn(bool forceResetAlpha = false)
        {
            if (!_initialized)
            {
                Initialize();
            }

            if (forceResetAlpha)
            {
                _currentAlpha = 255;
            }

            _activateFadeIn = true;
            _activateFadeOut = false;
        }

        private void FadeIn()
        {
            _currentAlpha -= fadeInRate * Time.deltaTime;

            Color fadeImageColor = _fadeImage.color;
            _fadeImage.color =
                ExtensionFunctions.ConvertAndClampColor(fadeImageColor.r, fadeImageColor.g, fadeImageColor.b,
                    _currentAlpha);

            if (!(_currentAlpha <= 0))
                return;

            OnFadeInComplete?.Invoke();
            _activateFadeIn = false;
            _fadeImage.gameObject.SetActive(false);
        }

        public void StartFadeOut(bool forceResetAlpha = false)
        {
            if (!_initialized)
            {
                Initialize();
            }

            if (forceResetAlpha)
            {
                _currentAlpha = 0;
            }

            _fadeImage.gameObject.SetActive(true);

            _activateFadeOut = true;
            _activateFadeIn = false;
        }

        private void FadeOut()
        {
            _currentAlpha += fadeOutRate * Time.deltaTime;

            Color fadeImageColor = _fadeImage.color;
            _fadeImage.color =
                ExtensionFunctions.ConvertAndClampColor(fadeImageColor.r, fadeImageColor.g, fadeImageColor.b,
                    _currentAlpha);

            if (!(_currentAlpha >= 255))
                return;

            OnFadeOutComplete?.Invoke();
            _activateFadeOut = false;
        }

        #endregion
    }
}