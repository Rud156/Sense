using UnityEngine;

namespace Audio
{
    public class SfxAudioManager : MonoBehaviour
    {
        public GameObject audioPrefab;
        public Transform audioHolder;
        public GameObject cameraPosition;

        #region External Functions

        public void PlaySound(AudioClip audioClip, float playDelay = 0)
        {
            GameObject audioInstance = Instantiate(audioPrefab, cameraPosition.transform.position, Quaternion.identity);
            audioInstance.transform.SetParent(audioHolder);

            SfxObject sfxObject = audioInstance.GetComponent<SfxObject>();
            sfxObject.PlayAudio(audioClip, playDelay);
        }

        public void PlaySound(AudioClip audioClip, Vector3 worldPosition, float playDelay = 0)
        {
            GameObject audioInstance = Instantiate(audioPrefab, worldPosition, Quaternion.identity);
            audioInstance.transform.SetParent(audioHolder);

            SfxObject sfxObject = audioInstance.GetComponent<SfxObject>();
            sfxObject.PlayAudio(audioClip, playDelay, true);
        }

        public void PlaySound(GameObject audioPrefab, float playDelay = 0)
        {
            GameObject audioInstance = Instantiate(audioPrefab, audioHolder.position, Quaternion.identity);
            audioInstance.transform.SetParent(audioHolder);

            SfxObject sfxObject = audioInstance.GetComponent<SfxObject>();
            sfxObject.PlayAudio(playDelay);
        }

        public void PlaySound(GameObject audioPrefab, Vector3 worldPosition, float playDelay = 0)
        {
            GameObject audioInstance = Instantiate(audioPrefab, worldPosition, Quaternion.identity);
            audioInstance.transform.SetParent(audioHolder);

            SfxObject sfxObject = audioInstance.GetComponent<SfxObject>();
            sfxObject.PlayAudio(playDelay, true);
        }

        #endregion

        #region Singleton

        private static SfxAudioManager _instance;
        public static SfxAudioManager Instance => _instance;

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