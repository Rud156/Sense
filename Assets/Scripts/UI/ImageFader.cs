using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class ImageFader : MonoBehaviour
    {
        public float fadeRate;

        private bool _isFadingActive;
        private bool _isFadingIn;
        private float _currentAlpha;

        private Image _affectorImage;
        private bool _initialized;

        #region Unity Functions

        private void Start()
        {
            if (!_initialized)
            {
                _affectorImage = GetComponent<Image>();
                _isFadingActive = true;
                _isFadingIn = false;
            }
        }

        private void Update()
        {
            if (!_isFadingActive)
            {
                return;
            }

            if (!_isFadingIn)
            {
                Color fadeOutColor = _affectorImage.color;
                fadeOutColor.a = _currentAlpha;
                _affectorImage.color = fadeOutColor;

                _currentAlpha -= Time.deltaTime * fadeRate;
                if (_currentAlpha <= 0)
                {
                    _isFadingIn = true;
                    _currentAlpha = 0;
                }
            }
            else
            {
                Color fadeInColor = _affectorImage.color;
                fadeInColor.a = _currentAlpha;
                _affectorImage.color = fadeInColor;

                _currentAlpha += fadeRate * Time.deltaTime;
                if (_currentAlpha >= 1)
                {
                    _isFadingIn = false;
                    _currentAlpha = 1;
                }
            }
        }

        #endregion

        #region External Functions

        public void StartFading()
        {
            if (!_initialized)
            {
                Initialize();
            }

            _isFadingActive = true;
        }

        public void StopFading(bool resetAlpha = true)
        {
            if (!_initialized)
            {
                Initialize();
            }

            _isFadingActive = false;

            Color imageColor = _affectorImage.color;
            imageColor.a = 0;
            _affectorImage.color = imageColor;
        }

        #endregion

        #region Utility Functions

        private void Initialize()
        {
            _affectorImage = GetComponent<Image>();
            _isFadingActive = true;
            _isFadingIn = false;

            _initialized = true;
        }

        #endregion
    }
}