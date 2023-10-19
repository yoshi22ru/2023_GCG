using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : Character
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
    private float skill1Cooldown = 5f; // Skill1�̃N�[���_�E������
    [SerializeField]
    private float skill2Cooldown = 8f; // Skill2�̃N�[���_�E������
    [SerializeField]
    private float specialCooldown = 10f; // Special�̃N�[���_�E������
    [SerializeField]
    private float skill1Damage = 20f; // Skill1�̃_���[�W��
    [SerializeField]
    private float skill2Damage = 30f; // Skill2�̃_���[�W��
    [SerializeField]
    private float specialDamage = 50f; // Special�̃_���[�W��

    private bool isDamageTaken = false; // �_���[�W���󂯂����ǂ���
    private bool isDead = false; // ���S����

    private float skill1CooldownTimer = 0f; // Skill1�̃N�[���_�E���^�C�}�[
    private float skill2CooldownTimer = 0f; // Skill2�̃N�[���_�E���^�C�}�[
    private float specialCooldownTimer = 0f; // Special�̃N�[���_�E���^�C�}�[

    public float CurrentHP
    {
        get { return currentHP; }
    }

    public bool IsDead
    {
        get { return isDead; }
    }

    public float MoveSpeed
    {
        get { return moveSpeed; }
    }

    // �L�����N�^�[�̏�Ԃ��X�V����
    public void UpdateStatus()
    {
        if (isDead)
            return;

        // Skill1�̃N�[���_�E���^�C�}�[���X�V
        if (skill1CooldownTimer > 0f)
        {
            skill1CooldownTimer -= Time.deltaTime;
        }

        // Skill2�̃N�[���_�E���^�C�}�[���X�V
        if (skill2CooldownTimer > 0f)
        {
            skill2CooldownTimer -= Time.deltaTime;
        }

        // Special�̃N�[���_�E���^�C�}�[���X�V
        if (specialCooldownTimer > 0f)
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
    public void UseSkill1()
    {
        if (skill1CooldownTimer <= 0f)
        {
            // �X�L���𔭓�
            Skill1();
            SetState(Character.Character_State.Skill1);

            // �N�[���_�E���^�C�}�[��ݒ�
            skill1CooldownTimer = skill1Cooldown;
        }
        else
        {
            Debug.Log((skill1CooldownTimer).ToString("F2") + "�b��ɃX�L��1�g�p�\");
        }
    }

    // �L�����N�^�[�̃X�L��2�𔭓�����
    public void UseSkill2()
    {
        if (skill2CooldownTimer <= 0f)
        {
            // �X�L���𔭓�
            Skill2();
            SetState(Character.Character_State.Skill2);

            // �N�[���_�E���^�C�}�[��ݒ�
            skill2CooldownTimer = skill2Cooldown;
        }
        else
        {

            Debug.Log((skill2CooldownTimer).ToString("F2") + "�b��ɃX�L��2�g�p�\");
        }
    }

    // �L�����N�^�[��Special�𔭓�����
    public void UseSpecial()
    {
        if (specialCooldownTimer <= 0f)
        {
            // Special�𔭓�
            Special();
            SetState(Character.Character_State.Special);

            // �N�[���_�E���^�C�}�[��ݒ�
            specialCooldownTimer = specialCooldown;
        }
        else
        {
            Debug.Log((specialCooldownTimer).ToString("F2") + "�b��ɃX�y�V�����X�L���g�p�\");
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
