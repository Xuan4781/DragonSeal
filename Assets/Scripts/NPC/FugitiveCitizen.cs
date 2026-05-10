using UnityEngine;
using DragonSeal.Data;

namespace DragonSeal.NPC
{
    public class FugitiveCitizen : CitizenNPC
    {
        [SerializeField] private float nervousSpeedMultiplier = 1.3f;

        protected override void Awake()
        {
            base.Awake();
            // nervous
            moveSpeed *= nervousSpeedMultiplier;
        }

        protected override void OnArrivedAtWindow()
        {
            base.OnArrivedAtWindow();
            Debug.Log($"{CitizenData.citizenName} is a fugitive. Acting suspicious.");
            PlayPleadAnimation();
        }
    }
}