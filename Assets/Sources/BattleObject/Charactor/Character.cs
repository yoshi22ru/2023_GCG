using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class Character : BattleObject
{
    public Animator animator;  
    private Character_State currentState; 
    private CharacterStatus characterStatus; 
    private float time;
  
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
        characterStatus = GetComponent<CharacterStatus>();
        animator = GetComponent<Animator>();
        SetState(Character_State.Idle);
    }

    public override void OnHitMyTeamObject(BattleObject gameObject)
    {
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
        SetState(Character_State.Damage);
    }

    

    private void FixedUpdate()
    {
        characterStatus.CheckDeath();
        characterStatus.UpdateStatus();

        if (characterStatus.IsDead)
        {
            time += Time.deltaTime;
            SetState(Character_State.Dead);
            if (time >= 1.5)
            {
                gameObject.SetActive(false);
                characterStatus.SetHP(characterStatus.MaxHP);
            }
            else if(time >= 10)
            {
                characterStatus.SetIsDead(false);
                gameObject.SetActive(true);
                time = 0;
            }
        }

        if (characterStatus.IsDead == false)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                SetState(Character_State.Run);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                if (characterStatus.UseSkill1())
                {
                    Skill1();
                }
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                if (characterStatus.UseSkill2())
                {
                    Skill2();
                }
            }
            else if (Input.GetKey(KeyCode.X))
            {
                if (characterStatus.UseSpecial())
                {
                    Special();
                }
            }
            else if (Input.GetKey(KeyCode.J) && Input.GetKey(KeyCode.K))
            { 
                characterStatus.SetIsDead(true);
            }
            else
            {
                SetState(Character_State.Idle);
            }
            characterStatus.CheckDeath();
        }
    }
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
}


