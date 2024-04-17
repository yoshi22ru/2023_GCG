using UnityEngine;

namespace Sources.InGame.SecondEdition.Entity
{
    public class BaseSkillEntity
    {
        public readonly int BaseDamage;
        public readonly GameObject SkillInstance; 
            
        public BaseSkillEntity(int baseDamage, GameObject skillInstance)
        {
            BaseDamage = baseDamage;
            SkillInstance = skillInstance;
        }
    }
}