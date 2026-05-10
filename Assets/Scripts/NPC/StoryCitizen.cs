using UnityEngine;
using DragonSeal.Data;

namespace DragonSeal.NPC
{
    public class StoryCitizen : CitizenNPC
    {
        protected override void OnArrivedAtWindow()
        {
            base.OnArrivedAtWindow();
            // maybe surprised with a luttle animation
            Debug.Log($"{CitizenData.citizenName} is a story citizen. They seem to recognize Hana.");
            PlaySurprisedAnimation();
        }
    }
}