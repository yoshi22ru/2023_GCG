using UnityEngine;


namespace Sources.InGame.BattleObject.Skill
{
    public class SkillManager : BattleObject
    {
        public float skillSpeed=5f;
        public float leftTime=5f;
        private float _currentTime;
        [SerializeField] int skillDamage=5;
        [SerializeField] int healValue=5;
        [SerializeField] float bufAttack=5f;
        [SerializeField] float bufSpeed=5f;

        public SkillType type;
        private Rigidbody _rb;
        
        // Temporary implementation
        public const float SPEED_UP_VALUE = 15f;
        public const float SPEED_UP_LENGTH = 15f;

        public const float ATTACK_UP_VALUE = 15f;
        public const float ATTACK_UP_LENGTH = 15f;
        

        public enum SkillType
        {
            Damage,
            Heal,
            BufAttack,
            BufSpeed,
        }


        public int GetHeal
        {
            get { return healValue; }
        }

        public int GetSkillDamage
        {
            get { return skillDamage; }
        }

        public float GetBufAttack
        {
            get { return bufAttack; }
        }

        public float GetBufSpeed
        {
            get { return bufSpeed; }
        }

        public void SetHeal(int heal)
        {
            if (heal > 0)
                healValue = heal;
        }

        public void SetSkillDamage(int skillATK)
        {
            if (skillATK > 0)
                skillDamage = skillATK;
        }

        public void SetBufAttack(float bufATK)
        {
            if (bufATK > 0)
                bufAttack = bufATK;
        }

        public void SetBufSpeed(float bufSPD)
        {
            if (bufSPD > 0)
                bufAttack = bufSPD;
        }



        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.velocity = transform.forward * skillSpeed;
            Destroy(gameObject, leftTime);
        }
    }
}