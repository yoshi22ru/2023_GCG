using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField]
    private float currentHP = 100f; // ���݂�HP
    [SerializeField]
    private float minHP = 0f; // �ŏ���HP
    [SerializeField]
    private float maxHP = 100f; // �ő��HP
    [SerializeField]
    private float moveSpeed = 5f; // �L�����N�^�[�̈ړ����x
    [SerializeField]
    private float attackPoint;
    [SerializeField]
    private float Skill1CoolDown = 5f;
    [SerializeField]
    private float Skill2CoolDown = 8f;
    [SerializeField]
    private float SpecialCoolDown = 10f;


    private bool isDamageTaken = false; // �_���[�W���󂯂����ǂ���
    private bool isDead = false; // ���S����

    private float skill1CooldownTimer = 0f; // Skill1�̃N�[���_�E���^�C�}�[
    private float skill2CooldownTimer = 0f; // Skill2�̃N�[���_�E���^�C�}�[
    private float specialCooldownTimer = 0f; // Special�̃N�[���_�E���^�C�}�[

    SkillManager skillManager = new SkillManager();

    public float CurrentHP
    {
        get { return currentHP; }
    }

    public float MaxHP
    {
        get { return maxHP; }
    }

    public bool IsDead
    {
        get { return isDead; }
    }

    public float MoveSpeed
    {
        get { return moveSpeed; }
    }

    public float AttackPoint
    {
        get { return attackPoint; }
    }

    public void SetHP(float hp)
    {
        if(hp > 0)
            currentHP = hp;
    }

    public void SetMoveSpeed(float speed)
    {
        if (speed > 0)
            moveSpeed = speed;
    }

    public void SetAttackPoint(float attack)
    {
        if (attack > 0)
            attackPoint = attack;
    }

    // �L�����N�^�[�̏�Ԃ��X�V����
    public void UpdateStatus()
    {
        if (isDead)
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

    // �L�����N�^�[���_���[�W���󂯂��Ƃ��ɌĂяo�����
    public void TakeDamage()
    {
        isDamageTaken = false;
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
    public void CheckDeath()
    {
        if (currentHP <= minHP)
        {
            isDead = true;
            currentHP = minHP;
        }
    }
}

