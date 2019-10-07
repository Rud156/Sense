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
            Instantiate(effectPrefab, playerPosition, Quaternion.identity);
            SfxAudioManager.Instance.PlaySound(collectionSound);

            return true;
        }
    }
}