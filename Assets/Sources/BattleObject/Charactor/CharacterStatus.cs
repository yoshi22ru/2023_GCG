using System.Collections;
using System.Collections.Generic;
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
            // �����Ŏ��ۂ̃X�L��1�̏������L�q

            // �N�[���_�E���^�C�}�[��ݒ�
            skill1CooldownTimer = skill1Cooldown;
        }
    }

    // �L�����N�^�[�̃X�L��2�𔭓�����
    public void UseSkill2()
    {
        if (skill2CooldownTimer <= 0f)
        {
            // �X�L���𔭓�
            // �����Ŏ��ۂ̃X�L��2�̏������L�q

            // �N�[���_�E���^�C�}�[��ݒ�
            skill2CooldownTimer = skill2Cooldown;
        }
    }

    // �L�����N�^�[��Special�𔭓�����
    public void UseSpecial()
    {
        if (specialCooldownTimer <= 0f)
        {
            // Special�𔭓�
            // �����Ŏ��ۂ�Special�̏������L�q

            // �N�[���_�E���^�C�}�[��ݒ�
            specialCooldownTimer = specialCooldown;
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
