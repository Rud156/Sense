using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace PlayerDecisions.DecisionModifiers
{
    public class EffectCollection : DecisionPointModifier
    {
        [Header("Effects")] public GameObject effectPrefab;

        public override bool AffectPlayer(Vector3 playerPosition, PlayerController playerController)
        {
            Instantiate(effectPrefab, playerPosition, Quaternion.identity);

            return true;
        }
    }
}