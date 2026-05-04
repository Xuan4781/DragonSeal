using UnityEngine;

namespace DragonSeal.Data
{
    public enum DragonClass { Class1, Class2, Class3, Class4, Class5, ClassS }
    public enum CitizenType { Regular, Story, Fugitive }

    [CreateAssetMenu(fileName = "NewCitizen", menuName = "DragonSeal/Citizen")]
    public class CitizenSO : ScriptableObject
    {
        [Header("Identity")]
        public string citizenName;
        public int age;
        public CitizenType citizenType;

        // doc info, scanner info, and if doc has problem
        [Header("Dragon Info")]
        public DragonClass certifiedClass;   
        public DragonClass actualClass;      
        public bool isForged;                

        // story hints
        [Header("Story")]
        public string storyHint;             
        public string openAIPersonality;     

        [Header("Visuals")]
        public string portraitKey;       
        public Sprite portrait;       
    }
}