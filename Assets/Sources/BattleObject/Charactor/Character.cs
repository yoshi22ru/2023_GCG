using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

public class Character : BattleObject, IPunObservable
{
    public byte actor_num;
    public Animator animator;  // �L�����N�^�[�̃A�j���[�^�[�R���|�[�l���g

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

    private Character_State currentState; // ���݂̏��

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
    }

    protected virtual void Skill2()
    {
        // Skill2��Ԃ̓�������s
        Debug.Log("�X�L��2����");
    }

    protected virtual void Special()
    {
        // Special��Ԃ̓�������s
        Debug.Log("�X�y�V�����X�L������");
    }

    void IPunObservable.OnPhotonSerializeView(Photon.Pun.PhotonStream stream, Photon.Pun.PhotonMessageInfo info) {
        if (stream.IsWriting) {
            // send
            PhotonNetwork.LocalPlayer.SetTransform(this.transform);
            PhotonNetwork.LocalPlayer.SetHp(getHP());
        } else {
            // receive
            Vector3 position;
            Quaternion rotation;
            long trans = PhotonNetwork.LocalPlayer.GetTransform();
            (position, rotation) = Protcol.TransformDeserialize(trans);
            this.transform.position = position;
            this.transform.rotation = rotation;
            SetHP(PhotonNetwork.LocalPlayer.GetHp());
        }
    }
}

