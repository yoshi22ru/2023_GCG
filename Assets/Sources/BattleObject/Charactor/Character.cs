using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class Character : BattleObject
{
    public Animator animator;  // �L�����N�^�[�̃A�j���[�^�[�R���|�[�l���g
    private Character_State currentState; // ���݂̏��
    private CharacterStatus characterStatus; // �L�����N�^�[�̃X�e�[�^�X
    // Transform characterPoint;
    //[SerializeField] GameObject skill1;
    //[SerializeField] GameObject skill2;
    //[SerializeField] GameObject special;
    //[SerializeField] Transform skill1Point;
    //[SerializeField] Transform skill2Point;
    //[SerializeField] Transform specialPoint;

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
        // �L�����N�^�[�̃X�e�[�^�X���擾�܂��͏�����
        characterStatus = GetComponent<CharacterStatus>();
        // �A�j���[�^�[�R���|�[�l���g���擾
        animator = GetComponent<Animator>();
        // �ŏ��̏�Ԃ�ݒ�
        SetState(Character_State.Idle);
    }

    private void FixedUpdate()
    {
        if (characterStatus.IsDead)
        {
            // �L�����N�^�[�����S���Ă���ꍇ�͏������I��
            return;
        }

        // �L�[���͂Ɋ�Â��ď�ԑJ�ڂ𐧌�
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            // WASD�L�[��������Ă���Ԃ�Run��ԂɑJ��
            characterStatus.UpdateStatus();
            SetState(Character.Character_State.Run);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // E�L�[���������ꍇ�ASkill1�𔭓�
            Skill1();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // Q�L�[���������ꍇ�ASkill2�𔭓�
            Skill2();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            // R�L�[���������ꍇ�ASpecial�𔭓�
            Special();
        }
        else
        {
            // �������͂���Ă��Ȃ��ꍇ��Idle��ԂɑJ��
            characterStatus.UpdateStatus();
            SetState(Character.Character_State.Idle);
        }

        // �L�����N�^�[�̎��S������s��
        characterStatus.CheckDeath();
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
        //Instantiate(skill1, skill1Point.position, transform.rotation);
        characterStatus.UseSkill1();
    }

    protected virtual void Skill2()
    {
        // Skill2��Ԃ̓�������s
        Debug.Log("�X�L��2����");
        //Instantiate(skill2, skill2Point.position, transform.rotation);
        characterStatus.UseSkill2();
    }

    protected virtual void Special()
    {
        // Special��Ԃ̓�������s
        Debug.Log("�X�y�V�����X�L������");
        //Instantiate(special, specialPoint.position, transform.rotation, characterPoint);
        characterStatus.UseSpecial();
    }
}


