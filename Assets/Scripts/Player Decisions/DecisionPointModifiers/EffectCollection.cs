using Audio;
using Player;
using UnityEngine;

namespace PlayerDecisions.DecisionModifiers
{
    public class EffectCollection : DecisionPointModifier
    {
        [Header("Effects")] public GameObject effectPrefab;
        public AudioClip collectionSound;

        public override bool AffectPlayer(Vector3 playerPosition, PlayerController playerController)
        {
            GameObject effectInstance = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            effectInstance.transform.localScale = Vector3.one * 5;
            SfxAudioManager.Instance.PlaySound(collectionSound);

            return true;
        }
    }
}