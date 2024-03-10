using UnityEngine;


namespace Sources.InGame.BattleObject.Skill
{
    public class SkillManager : BattleObject
    {
        public float skillSpeed;
        public float leftTime;
        private float _currentTime;
        [SerializeField] int skillDamage;
        [SerializeField] int healValue;
        [SerializeField] float bufAttack;
        [SerializeField] float bufSpeed;

        public SkillType type;

        public enum SkillType
        {
            damage,
            heal,
            bufAttack,
            bufSpeed,
        }

        Rigidbody rb;

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