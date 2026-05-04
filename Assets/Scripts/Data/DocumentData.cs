using UnityEngine;

namespace DragonSeal.Data
{
    public struct DocumentData
    {
        public string citizenName;
        public int age;
        public DragonClass certifiedClass;
        public bool hasValidLicense;
        public bool hasValidAppointment;
        public bool isExpired;

        public DocumentData(CitizenSO citizen)
        {
            citizenName = citizen.citizenName;
            age = citizen.age;
            certifiedClass = citizen.certifiedClass;
            hasValidLicense = !citizen.isForged;
            hasValidAppointment = !citizen.isForged;
            isExpired = false;
        }
    }
}