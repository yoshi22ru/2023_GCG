using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class Character : BattleObject, IPunObservable
{
    public Animator animator;
    protected Character_State currentState;
    protected CharacterStatus characterStatus;
    protected float time;
    [SerializeField] protected Transform my_transform;
    protected Vector3 p1, p2, v1, v2;
    protected half elapsed_time;
    protected const float InterpolationPeriod = 0.1f;

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

    private void Awake()
    {
        characterStatus = GetComponent<CharacterStatus>();
        animator = GetComponent<Animator>();
        SetState(Character_State.Idle);
    }
    [PunRPC]
    public void Initialize(Team team, GameManager gameManager) {
        SetTeam(team);
        setManager(GameManager.manager);

        manager.AddPlayer(this, characterStatus, PhotonNetwork.LocalPlayer.ActorNumber, team);
    }

    [PunRPC]
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
                skillManager.SetSkill2Damage(skillManager.GetSkill2Damage + 15);
                skillManager.SetSpecialDamage(skillManager.GetSpecialDamage + 15);
            }
        }
    }

    [PunRPC]
    public override void OnHitEnemyTeamObject(BattleObject gameObject)
    {
        if (!photonView.IsMine) return;

        SkillManager skillManager = gameObject as SkillManager;
        if (skillManager == null)
            return;
        if(skillManager.type == SkillManager.SkillType.weekDamage)
        {
            characterStatus.SetHP(characterStatus.CurrentHP - skillManager.GetSkill1Damage);
        }
        else if(skillManager.type == SkillManager.SkillType.midDamage)
        {
            characterStatus.SetHP(characterStatus.CurrentHP - skillManager.GetSkill2Damage);
        }
        else if(skillManager.type == SkillManager.SkillType.strongDamage)
        {
            characterStatus.SetHP(characterStatus.CurrentHP - skillManager.GetSpecialDamage);
        }

        photonView.RPC(nameof(SetState), RpcTarget.All, Character_State.Damage);
        // SetState(Character_State.Damage);
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
            time += Time.deltaTime;

            if (currentState != Character_State.Dead) {
                photonView.RPC(nameof(SetState), RpcTarget.All, Character_State.Dead);
                // SetState(Character_State.Dead);
            }
            characterStatus.SetHP(characterStatus.MaxHP);
        }

        if (characterStatus.IsDead == false && CountDown.instance.isCountFinish == true)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if (currentState != Character_State.Run) {
                    photonView.RPC(nameof(SetState), RpcTarget.All, Character_State.Run);
                    // SetState(Character_State.Run);
                }
            }
            else if (Input.GetKey(KeyCode.E))
            {
                if (characterStatus.UseSkill1())
                {
                    photonView.RPC(nameof(Skill1), RpcTarget.All);
                    // Skill1();
                }
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                if (characterStatus.UseSkill2())
                {
                    photonView.RPC(nameof(Skill2), RpcTarget.All);
                    // Skill2();
                }
            }
            else if (Input.GetKey(KeyCode.X))
            {
                if (characterStatus.UseSpecial())
                {
                    photonView.RPC(nameof(Special), RpcTarget.All);
                    // Special();
                }
            }
            else if (Input.GetKey(KeyCode.J) && Input.GetKey(KeyCode.K))
            {
                characterStatus.SetIsDead(true);
            }
            else
            {
                if (currentState != Character_State.Idle) {
                    photonView.RPC(nameof(SetState), RpcTarget.All, Character_State.Idle);
                    // SetState(Character_State.Idle);
                }
            }
        }
    }
    [PunRPC]
    public void SetState(Character_State newState)
    {
        currentState = newState;

        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Damage");
        animator.ResetTrigger("Dead");
        animator.ResetTrigger("Skill1");
        animator.ResetTrigger("Skill2");
        animator.ResetTrigger("Special");

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


