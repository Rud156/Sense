using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Player
{
    public class PlayerSenseController : MonoBehaviour
    {
        [Header("Seeing Sense")] public Light playerLight;
        public float minLightRange;
        public float maxLightRange;
        public PostProcessVolume postProcessVolume;

        [Header("Hearing Sense")] public AudioListener audioListener;

        private bool _hasHeatResistance;
        private bool _hasColdResistance;
        private bool _canPlayerHear;
        private bool _hasGrayScaleSight;
        private bool _hasColoredSight;

        private ColorGrading _cameraColorGrading;
        private Grain _cameraGrain;

        #region Unity Functions

        private void Start()
        {
            playerLight.range = minLightRange;
            audioListener.enabled = false;

            _cameraColorGrading = postProcessVolume.profile.GetSetting<ColorGrading>();
            _cameraGrain = postProcessVolume.profile.GetSetting<Grain>();

            _cameraColorGrading.saturation.value = -100;
            _cameraGrain.intensity.value = 1;
            _cameraGrain.size.value = 3;
        }

        #endregion

        #region External Functions

        public void CollectHearingSense()
        {
            audioListener.enabled = true;
            _canPlayerHear = true;
        }

        public bool CanPlayerHear => _canPlayerHear;

        public void CollectHeatResistance() => _hasHeatResistance = true;

        public bool HasHeatResistance => _hasHeatResistance;

        public void CollectColdResistance() => _hasColdResistance = true;

        public bool HasColdResistance => _hasColdResistance;

        public void CollectGrayScaleSight()
        {
            if (_hasColoredSight)
            {
                return;
            }

            playerLight.range = maxLightRange;
            _cameraColorGrading.saturation.value = -100;
            _cameraGrain.intensity.value = 1;
            _cameraGrain.size.value = 3;

            _hasGrayScaleSight = true;
        }

        public bool HasGrayScaleSight => _hasGrayScaleSight;

        public void CollectColoredSight()
        {
            playerLight.range = maxLightRange;
            _cameraColorGrading.saturation.value = 0;
            _cameraGrain.intensity.value = 0;
            _cameraGrain.size.value = 0.3f;

            _hasGrayScaleSight = true;
            _hasColoredSight = true;
        }

        public bool HasColoredSight => _hasColoredSight;

        #endregion
    }
}