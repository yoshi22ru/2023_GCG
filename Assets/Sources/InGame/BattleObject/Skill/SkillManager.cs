using UnityEngine;


namespace Sources.InGame.BattleObject.Skill
{
    public class SkillManager : BattleObject
    {
        public float skillSpeed;
        public float leftTime;
        private float _currentTime;
        [SerializeField] int skill1Damage;
        [SerializeField] int skill2Damage;
        [SerializeField] int specialDamage;
        [SerializeField] int healValue;
        [SerializeField] float bufAttack;
        [SerializeField] float bufSpeed;

        public SkillType type;

        public enum SkillType
        {
            weekDamage,
            midDamage,
            strongDamage,
            heal,
            bufAttack,
            bufSpeed,
        }

        Rigidbody rb;

        public int GetHeal
        {
            get { return healValue; }
        }

        public int GetSkill1Damage
        {
            get { return skill1Damage; }
        }

        public int GetSkill2Damage
        {
            get { return skill2Damage; }
        }

        public int GetSpecialDamage
        {
            get { return specialDamage; }
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

        public void SetSkill1Damage(int skill1)
        {
            if (skill1 > 0)
                skill1Damage = skill1;
        }

        public void SetSkill2Damage(int skill2)
        {
            if (skill2 > 0)
                skill2Damage = skill2;
        }

        public void SetSpecialDamage(int special)
        {
            if (special > 0)
                specialDamage = special;
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
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * skillSpeed;
        }

        void FixedUpdate()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime > leftTime)
            {
                Destroy(gameObject);
                _currentTime = 0;
            }
        }
    }
}