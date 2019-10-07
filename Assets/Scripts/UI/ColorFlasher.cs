using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ColorFlasher : MonoBehaviour
    {
        public float colorFlashTime = 0.5f;
        public int colorFlashCount = 5;
        public Color defaultColor;
        public Image flashImage;

        private bool _flashActive;
        private float _currentFlashTime;
        private int _currentFlashCount;

        #region Unity Functions

        private void Update()
        {
            if (!_flashActive)
            {
                return;
            }

            _currentFlashTime -= Time.deltaTime;
            if (_currentFlashTime <= 0)
            {
                _currentFlashCount -= 1;
                _currentFlashTime = colorFlashTime;

                flashImage.color = Random.ColorHSV();

                if (_currentFlashCount <= 0)
                {
                    StopFlashing();
                }
            }
        }

        #endregion

        #region External Functions

        public void StartFlashing()
        {
            _flashActive = true;
            _currentFlashTime = colorFlashTime;
            _currentFlashCount = colorFlashCount;
        }

        #endregion

        #region Utility Functions

        private void StopFlashing()
        {
            _flashActive = false;
            flashImage.color = defaultColor;
        }

        #endregion
    }
}