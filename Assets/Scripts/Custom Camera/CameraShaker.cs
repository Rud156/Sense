using UnityEngine;

namespace CustomCamera
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private Transform _mainCamera;

        private Vector3 _shakePosition;
        private float _shakeMultiplier;
        private float _shakeFrequency;

        private bool _isShakeActive;
        private float _shakeTimer;

        #region Unity Functions

        private void Start() => _shakePosition = Vector3.zero;

        private void Update()
        {
            if (!_isShakeActive)
            {
                return;
            }

            _shakeTimer -= Time.deltaTime;
            if (_shakeTimer <= 0)
            {
                StopShaking();
            }
            else
            {
                UpdateShaking();
            }
        }

        private void UpdateShaking()
        {
            _shakePosition.Set(
                Random.Range(-_shakeMultiplier, _shakeMultiplier),
                Random.Range(-_shakeMultiplier, _shakeMultiplier),
                Random.Range(-_shakeMultiplier, _shakeMultiplier)
            );
            _mainCamera.position = _shakePosition;
        }

        #endregion

        #region External Functions

        public void StartShaking(float shakeTime, float shakeMultiplier, float shakeFrequency)
        {
            _isShakeActive = true;
            _shakeTimer = shakeTime;

            _shakeMultiplier = shakeMultiplier;
            _shakeFrequency = shakeFrequency;
        }

        public void StopShaking()
        {
            _isShakeActive = false;

            _shakePosition.Set(0, 0, 0);
            _mainCamera.position = _shakePosition;
        }

        #endregion

        #region Singleton

        private static CameraShaker _instance;
        public static CameraShaker Instance => _instance;

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