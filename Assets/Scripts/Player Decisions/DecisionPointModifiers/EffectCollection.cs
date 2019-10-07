using Audio;
using Player;
using PlayerDecisions.DecisionItemData;
using UnityEngine;

namespace PlayerDecisions.DecisionModifiers
{
    public class EffectCollection : DecisionPointModifier
    {
        [Header("Effects")] public GameObject effectPrefab;
        public AudioClip collectionSound;

        public override bool AffectPlayer(Vector3 playerPosition, PlayerController playerController, DecisionItem decisionItem)
        {
            if (!decisionItem.IsItemCollected())
            {
                GameObject effectInstance = Instantiate(effectPrefab, transform.position, effectPrefab.transform.rotation);
                effectInstance.transform.localScale = Vector3.one * 10;
                SfxAudioManager.Instance.PlaySound(collectionSound);
            }

            return true;
        }
    }
}