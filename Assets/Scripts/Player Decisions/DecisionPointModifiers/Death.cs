using Audio;
using Player;
using PlayerDecisions.DecisionItemData;
using UnityEngine;

namespace PlayerDecisions.DecisionModifiers
{
    public class Death : DecisionPointModifier
    {
        public AudioClip deathAudioClip;

        public override bool AffectPlayer(Vector3 playerPosition, PlayerController playerController, DecisionItem decisionItem)
        {
            SfxAudioManager.Instance.PlaySound(deathAudioClip);
            playerController.PlayDeathAnimation();

            return false;
        }
    }
}