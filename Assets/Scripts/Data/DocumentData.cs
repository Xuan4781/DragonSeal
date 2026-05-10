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
            citizenName = citizen.documentName;
            age = citizen.documentAge;
            certifiedClass = citizen.documentClass;
            hasValidLicense = !citizen.isForged;
            hasValidAppointment = citizen.documentHasAppointment;
            isExpired = false;
        }
    }
}