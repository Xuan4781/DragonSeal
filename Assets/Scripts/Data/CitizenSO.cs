using UnityEngine;

namespace DragonSeal.Data
{
    public enum DragonClass { Class1, Class2, Class3, Class4, Class5, ClassS }
    public enum CitizenType { Regular, Story, Fugitive }
    public enum Gender { Male, Female, Other }
    public enum Region { NorthDistrict, SouthDistrict, EastDistrict, WestDistrict, Capital }

    [CreateAssetMenu(fileName = "NewCitizen", menuName = "DragonSeal/Citizen")]
    public class CitizenSO : ScriptableObject
    {
        [Header("Identity")]
        public string citizenName;
        public int age;
        public CitizenType citizenType;

        [Header("Government Database (True Data)")]
        public DragonClass trueClass;
        public bool isRegistered;
        public string appointmentDate;
        public Gender trueGender;
        public Region trueRegion;
        public string trueExpiryDate;

        [Header("Citizen Document (What They Hand You)")]
        public DragonClass documentClass;
        public string documentName;
        public int documentAge;
        public bool documentHasAppointment;
        public Gender documentGender;
        public Region documentRegion;
        public string documentExpiryDate;
        public bool isForged;

        [Header("Story")]
        public string storyHint;
        public string openAIPersonality;

        [Header("Visuals")]
        public string portraitKey;
        public Sprite portrait;
        public Sprite documentPhoto;
    }
}