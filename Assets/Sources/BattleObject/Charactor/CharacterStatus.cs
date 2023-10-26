using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class CharacterStatus : Character
{
    [SerializeField]
    private float currentHP = 100f; // 現在のHP
    [SerializeField]
    private float minHP = 0f; // 最小のHP
    [SerializeField]
    private float maxHP = 100f; // 最大のHP
    [SerializeField]
    private float moveSpeed = 5f; // キャラクターの移動速度
    [SerializeField]
    private float Skill1CoolDown = 5f;
    [SerializeField]
    private float Skill2CoolDown = 8f;
    [SerializeField]
    private float SpecialCoolDown = 10f;


    private bool isDamageTaken = false; // ダメージを受けたかどうか
    private bool isDead = false; // 死亡判定

    private float skill1CooldownTimer = 0f; // Skill1のクールダウンタイマー
    private float skill2CooldownTimer = 0f; // Skill2のクールダウンタイマー
    private float specialCooldownTimer = 0f; // Specialのクールダウンタイマー

    SkillManager skillManager = new SkillManager();

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

    // キャラクターの状態を更新する
    public void UpdateStatus()
    {
        if (isDead)
            return;

        // Skill1のクールダウンタイマーを更新
        if (skill1CooldownTimer > 0f)
        {
            skill1CooldownTimer -= Time.deltaTime;
        }

        // Skill2のクールダウンタイマーを更新
        if (skill2CooldownTimer > 0f)
        {
            skill2CooldownTimer -= Time.deltaTime;
        }

        // Specialのクールダウンタイマーを更新
        if (specialCooldownTimer > 0f)
        {
            specialCooldownTimer -= Time.deltaTime;
        }
    }

    // キャラクターがダメージを受けたときに呼び出される
    public void TakeDamage()
    {
        isDamageTaken = false;
    }

    // キャラクターのスキル1を発動する
    public void UseSkill1()
    {
        if (skill1CooldownTimer <= 0f)
        {
            // スキルを発動
            Skill1();
            SetState(Character.Character_State.Skill1);

            // クールダウンタイマーを設定
            skill1CooldownTimer = Skill1CoolDown;
        }
        else
        {
            Debug.Log((skill1CooldownTimer).ToString("F2") + "秒後にスキル1使用可能");
        }
    }

    // キャラクターのスキル2を発動する
    public void UseSkill2()
    {
        if (skill2CooldownTimer <= 0f)
        {
            // スキルを発動
            Skill2();
            SetState(Character.Character_State.Skill2);

            // クールダウンタイマーを設定
            skill2CooldownTimer = Skill2CoolDown;
        }
        else
        {

            Debug.Log((skill2CooldownTimer).ToString("F2") + "秒後にスキル2使用可能");
        }
    }

    // キャラクターのSpecialを発動する
    public void UseSpecial()
    {
        if (specialCooldownTimer <= 0f)
        {
            // Specialを発動
            Special();
            SetState(Character.Character_State.Special);

            // クールダウンタイマーを設定
            specialCooldownTimer = SpecialCoolDown;
        }
        else
        {
            Debug.Log((specialCooldownTimer).ToString("F2") + "秒後にスペシャルスキル使用可能");
        }
    }

    // キャラクターの死亡判定を行う
    public void CheckDeath()
    {
        if (currentHP <= minHP)
        {
            isDead = true;
            currentHP = minHP;
        }
    }
}

