using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.TextCore.Text;


public class Character : BattleObject, IPunObservable
{
    public Animator animator;  // �L�����N�^�[�̃A�j���[�^�[�R���|�[�l���g
    private Character_State currentState; // ���݂̏��
    private CharacterStatus characterStatus; // �L�����N�^�[�̃X�e�[�^�X
    [SerializeField] GameObject character;
    [SerializeField] Transform my_transform;
    Vector3 p1, p2, v1, v2;
    half elapsed_time;
    private const float InterpolationPeriod = 0.1f;
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

        p1 = my_transform.position;
        p2 = p1;
        v1 = Vector3.zero;
        v2 = v1;
    }

    public override void OnHitMyTeamObject(BattleObject gameObject)
    {
        if (!photonView.IsMine) return;

        SkillManager skillManager = gameObject as SkillManager;
        if (skillManager == null)
            return;
        if (skillManager.type == SkillManager.SkillType.heal)
        {
            characterStatus.SetHP(skillManager.GetHeal + characterStatus.CurrentHP);
        }
        else if (skillManager.type == SkillManager.SkillType.bufSpeed)
        {
            characterStatus.SetMoveSpeed(skillManager.GetBufSpeed + characterStatus.MoveSpeed);
        }
        else if (skillManager.type == SkillManager.SkillType.bufAttack)
        {
            GameObject[] skill = characterStatus.GetSkillPrefab;
            for (int i = 0; i < skill.Length; i++)
            {
                skillManager = skill[i].gameObject.GetComponent<SkillManager>();
                skillManager.SetSkill1Damage(skillManager.GetSkill1Damage + 15);
                skillManager.SetSkill1Damage(skillManager.GetSkill2Damage + 15);
                skillManager.SetSkill1Damage(skillManager.GetSpecialDamage + 15);
            }
        }
    }

    public override void OnHitEnemyTeamObject(BattleObject gameObject)
    {
        if (!photonView.IsMine) return;

        SkillManager skillManager = gameObject as SkillManager;
        if (skillManager == null)
            return;
        if(skillManager.type == SkillManager.SkillType.weekDamage)
        {
            int damage = skillManager.GetSkill1Damage;
            characterStatus.SetHP(characterStatus.CurrentHP - skillManager.GetSkill1Damage);
        }
        else if(skillManager.type == SkillManager.SkillType.midDamage)
        {
            int damage = skillManager.GetSkill2Damage;
            characterStatus.SetHP(characterStatus.CurrentHP - skillManager.GetSkill2Damage);
        }
        else if(skillManager.type == SkillManager.SkillType.strongDamage)
        {
            int damage = skillManager.GetSpecialDamage;
            characterStatus.SetHP(characterStatus.CurrentHP - skillManager.GetSpecialDamage);
        }
    }
    private void FixedUpdate()
    {
        if (photonView.IsMine) {
            p1 = p2;
            p2 = my_transform.position;
            elapsed_time = (half) Time.deltaTime;
        } else {
            elapsed_time += (half) Time.deltaTime;
            if (elapsed_time < InterpolationPeriod) {
                my_transform.position = HermiteSpline.Interpolate(p1, p2, v1, v2, elapsed_time/InterpolationPeriod);
            } else {
                my_transform.position = Vector3.LerpUnclamped(p1, p2, elapsed_time/InterpolationPeriod);
            }
        }

        if (characterStatus.IsDead)
        {
            character.SetActive(false);
            return;
        }
        characterStatus.UpdateStatus();

        // �L�[���͂Ɋ�Â��ď�ԑJ�ڂ𐧌�
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            // WASD�L�[��������Ă���Ԃ�Run��ԂɑJ��
            SetState(Character_State.Run);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            // Debug.Log("input skill1");
            if (characterStatus.UseSkill1())
            {
                // E�L�[���������ꍇ�ASkill1�𔭓�
                Skill1();
            }
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            // Debug.Log("input skill2");
            if (characterStatus.UseSkill2())
            {
                // Q�L�[���������ꍇ�ASkill2�𔭓�
                Skill2();
            }
        }
        else if (Input.GetKey(KeyCode.X))
        {
            // Debug.Log("input special");
            if(characterStatus.UseSpecial())
            {
                // R�L�[���������ꍇ�ASpecial�𔭓�
                Special();
            }
        }
        else
        {
            // �������͂���Ă��Ȃ��ꍇ��Idle��ԂɑJ��
            SetState(Character_State.Idle);
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

    protected virtual void BufSpeed(float speedUp)
    {

    }

    protected virtual void BufDamage(float damage)
    {

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
        characterStatus.UseSkill1();
        SetState(Character_State.Skill1);
    }

    protected virtual void Skill2()
    {
        characterStatus.UseSkill2();
        SetState(Character_State.Skill2);
    }

    protected virtual void Special()
    {
        characterStatus.UseSpecial();
        SetState(Character_State.Special);
    }

    void IPunObservable.OnPhotonSerializeView(Photon.Pun.PhotonStream stream, Photon.Pun.PhotonMessageInfo info) {
        if (stream.IsWriting) {
            // send
            short vx,vy,vz;
            (vx, vy, vz) = Protcol.PositionSerialize(my_transform.position);
            stream.SendNext(vx); stream.SendNext(vy); stream.SendNext(vz);
            (vx, vy, vz) = Protcol.PositionSerialize((p2 - p1) / elapsed_time);
            stream.SendNext(vx); stream.SendNext(vy); stream.SendNext(vz);
            stream.SendNext(Protcol.RotationSerialize(my_transform.rotation));
        } else {
            // receive

            // https://zenn.dev/o8que/books/bdcb9af27bdd7d/viewer/1ea062
            short px = (short) stream.ReceiveNext();
            short py = (short) stream.ReceiveNext();
            short pz = (short) stream.ReceiveNext();
            Vector3 net_pos = Protcol.PositionDeserialise(px, py, pz);
            px = (short) stream.ReceiveNext();
            py = (short) stream.ReceiveNext();
            pz = (short) stream.ReceiveNext();
            Vector3 net_vel = Protcol.PositionDeserialise(px, py, pz);

            my_transform.rotation = Protcol.RotationDeserialize((byte) stream.ReceiveNext());
            float lag = Mathf.Max(0f, unchecked(PhotonNetwork.ServerTimestamp - info.SentServerTimestamp) / 1000f);

            p1 = my_transform.position;
            p2 = net_pos + net_vel * lag;
            v1 = v2;
            v2 = net_vel * InterpolationPeriod;
            elapsed_time = (half) 0;
        }
    }
}


