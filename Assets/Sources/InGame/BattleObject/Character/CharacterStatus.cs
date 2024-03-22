using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;


namespace Sources.InGame.BattleObject.Character
{
    public class CharacterStatus : MonoBehaviour
    {
        [SerializeField] private int minHP = 0; // �ŏ���HP
        [SerializeField] private int maxHP = 100; // �ő��HP
        [SerializeField] private float moveSpeed = 5f; // �L�����N�^�[�̈ړ����x
        [SerializeField] private float Skill1CoolDown = 5f;
        [SerializeField] private float Skill2CoolDown = 8f;
        [SerializeField] private float SpecialCoolDown = 10f;
        [SerializeField] GameObject[] skillPrefab;
        private BuffManager _buffManager;

        private bool isDamageTaken = false; // �_���[�W���󂯂����ǂ���

        private float skill1CooldownTimer = 0f; // Skill1�̃N�[���_�E���^�C�}�[
        private float skill2CooldownTimer = 0f; // Skill2�̃N�[���_�E���^�C�}�[
        private float specialCooldownTimer = 0f; // Special�̃N�[���_�E���^�C�}�[

        private HitPoint _hitPoint;
        
        public GameObject[] GetSkillPrefab
        {
            get { return skillPrefab; }
        }

        public void SetSkillPrefab(GameObject[] skill)
        {
            skillPrefab = skill;
        }

        public int CurrentHP
        {
            get { return _hitPoint.Hp.CurrentValue; }
        }

        public int MaxHP
        {
            get { return maxHP; }
        }

        public int MinHP
        {
            get { return minHP; }
        }

        public bool IsDead
        {
            get { return _hitPoint.IsDead.CurrentValue; }
        }

        public float MoveSpeed
        {
            get { return moveSpeed; }
        }

        public void SetHP(int value)
        {
            _hitPoint.Damage(value);
        }

        public void SetMoveSpeed(float speed)
        {
            if (speed > 0)
                moveSpeed = speed;
        }

        private void Start()
        {
            _buffManager = new BuffManager();
            _hitPoint = new HitPoint(maxHP);
        }

        // �L�����N�^�[�̏�Ԃ��X�V����
        private void Update()
        {
            UpdateStatus();
        }

        public void UpdateStatus()
        {
            if (IsDead)
                return;

            // Skill1�̃N�[���_�E���^�C�}�[���X�V
            if (skill1CooldownTimer >= 0f)
            {
                skill1CooldownTimer -= Time.deltaTime;
            }

            // Skill2�̃N�[���_�E���^�C�}�[���X�V
            if (skill2CooldownTimer >= 0f)
            {
                skill2CooldownTimer -= Time.deltaTime;
            }

            // Special�̃N�[���_�E���^�C�}�[���X�V
            if (specialCooldownTimer >= 0f)
            {
                specialCooldownTimer -= Time.deltaTime;
            }
        }

        // �L�����N�^�[�̃X�L��1�𔭓�����
        public bool UseSkill1()
        {
            if (skill1CooldownTimer <= 0f)
            {
                Debug.Log("use skill1");
                // �N�[���_�E���^�C�}�[��ݒ�
                skill1CooldownTimer = Skill1CoolDown;
                return true;
            }
            else
            {
                Debug.Log("cool time of skill1" + (skill1CooldownTimer).ToString("F2"));
                return false;
            }
        }

        // �L�����N�^�[�̃X�L��2�𔭓�����
        public bool UseSkill2()
        {
            if (skill2CooldownTimer <= 0f)
            {
                Debug.Log("use skill2");
                // �N�[���_�E���^�C�}�[��ݒ�
                skill2CooldownTimer = Skill2CoolDown;
                return true;
            }
            else
            {
                Debug.Log("cool time of skill2" + (skill2CooldownTimer).ToString("F2"));
                return false;
            }
        }

        // �L�����N�^�[��Special�𔭓�����
        public bool UseSpecial()
        {
            if (specialCooldownTimer <= 0f)
            {
                Debug.Log("use special");
                // �N�[���_�E���^�C�}�[��ݒ�
                specialCooldownTimer = SpecialCoolDown;
                return true;
            }
            else
            {
                Debug.Log("cool time of special" + (specialCooldownTimer).ToString("F2"));
                return false;
            }
        }

        // �L�����N�^�[�̎��S������s��

        public float GetSkill1Cool()
        {
            return Skill1CoolDown;
        }

        public float GetSkill2Cool()
        {
            return Skill2CoolDown;
        }

        public float GetSpecialCool()
        {
            return SpecialCoolDown;
        }

        public float GetCurrentSkill1Cool()
        {
            return skill1CooldownTimer;
        }

        public float GetCurrentSkill2Cool()
        {
            return skill2CooldownTimer;
        }

        public float GetCurrentSpecialCool()
        {
            return specialCooldownTimer;
        }
    }

}