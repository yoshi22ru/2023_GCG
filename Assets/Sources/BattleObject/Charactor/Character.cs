using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Character : BattleObject
{
    public Animator animator;  // �L�����N�^�[�̃A�j���[�^�[�R���|�[�l���g
    private Character_State currentState; // ���݂̏��
    [SerializeField] GameObject skill1;
    [SerializeField] GameObject skill2;
    [SerializeField] GameObject special;
    [SerializeField] Transform shotPoint;
    [SerializeField] Transform centerPoint;

    // �L�����N�^�[�̏�Ԃ��`
    public enum Character_State
    {
        None,
        Idle,
        Run,
        Damage,
        Dead,
        Skill1,
        Skill2,
        Special,
    }

    private void Start()
    {
        // �A�j���[�^�[�R���|�[�l���g���擾
        animator = GetComponent<Animator>();
        // �ŏ��̏�Ԃ�ݒ�
        SetState(Character_State.Idle);
    }

    // �L�����N�^�[�̏�Ԃ�ݒ肵�A�g���K�[�𔭓����郁�\�b�h
    public void SetState(Character_State newState)
    {
        // ���݂̏�Ԃ�ݒ�
        currentState = newState;

        // �A�j���[�^�[�̃g���K�[�����Z�b�g
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Damage");
        animator.ResetTrigger("Dead");
        animator.ResetTrigger("Skill1");
        animator.ResetTrigger("Skill2");
        animator.ResetTrigger("Special");

        // �V������Ԃɉ����ăg���K�[��ݒ�
        switch (newState)
        {
            case Character_State.Idle:
                animator.SetTrigger("Idle");
                break;
            case Character_State.Run:
                animator.SetTrigger("Run");
                break;
            case Character_State.Damage:
                animator.SetTrigger("Damage");
                break;
            case Character_State.Dead:
                animator.SetTrigger("Dead");
                break;
            case Character_State.Skill1:
                animator.SetTrigger("Skill1");
                break;
            case Character_State.Skill2:
                animator.SetTrigger("Skill2");
                break;
            case Character_State.Special:
                animator.SetTrigger("Special");
                break;
        }
    }

    protected virtual void Damage(int damage)
    {
        // Damage��Ԃ̓�������s
        Debug.Log(damage + "�_���[�W������");
    }

    protected virtual void Heal(int heal)
    {
        Debug.Log(heal + "��");
    }

    protected virtual void Dead()
    {
        // Dead��Ԃ̓�������s
        Debug.Log("���S");
    }

    protected virtual void Skill1()
    {
        // Skill1��Ԃ̓�������s
        Debug.Log("�X�L��1����");
        Instantiate(skill1, shotPoint.position, transform.rotation);
    }

    protected virtual void Skill2()
    {
        // Skill2��Ԃ̓�������s
        Debug.Log("�X�L��2����");
        Instantiate(skill2, centerPoint.position, transform.rotation);
    }

    protected virtual void Special()
    {
        // Special��Ԃ̓�������s
        Debug.Log("�X�y�V�����X�L������");
        Instantiate(special, shotPoint.position, transform.rotation);
    }
}


