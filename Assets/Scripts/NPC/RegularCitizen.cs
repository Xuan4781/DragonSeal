using UnityEngine;
using DragonSeal.Data;

namespace DragonSeal.NPC
{
    public class RegularCitizen : CitizenNPC
    {
        protected override void OnArrivedAtWindow()
        {
            base.OnArrivedAtWindow();
            // calm
            Debug.Log($"{CitizenData.citizenName} is a regular citizen. Waiting patiently.");
        }
    }
}