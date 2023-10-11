using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
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
    private float skill1Cooldown = 5f; // Skill1のクールダウン時間
    [SerializeField]
    private float skill2Cooldown = 8f; // Skill2のクールダウン時間
    [SerializeField]
    private float specialCooldown = 10f; // Specialのクールダウン時間
    [SerializeField]
    private float skill1Damage = 20f; // Skill1のダメージ量
    [SerializeField]
    private float skill2Damage = 30f; // Skill2のダメージ量
    [SerializeField]
    private float specialDamage = 50f; // Specialのダメージ量

    private bool isDamageTaken = false; // ダメージを受けたかどうか
    private bool isDead = false; // 死亡判定

    private float skill1CooldownTimer = 0f; // Skill1のクールダウンタイマー
    private float skill2CooldownTimer = 0f; // Skill2のクールダウンタイマー
    private float specialCooldownTimer = 0f; // Specialのクールダウンタイマー

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
            // ここで実際のスキル1の処理を記述

            // クールダウンタイマーを設定
            skill1CooldownTimer = skill1Cooldown;
        }
    }

    // キャラクターのスキル2を発動する
    public void UseSkill2()
    {
        if (skill2CooldownTimer <= 0f)
        {
            // スキルを発動
            // ここで実際のスキル2の処理を記述

            // クールダウンタイマーを設定
            skill2CooldownTimer = skill2Cooldown;
        }
    }

    // キャラクターのSpecialを発動する
    public void UseSpecial()
    {
        if (specialCooldownTimer <= 0f)
        {
            // Specialを発動
            // ここで実際のSpecialの処理を記述

            // クールダウンタイマーを設定
            specialCooldownTimer = specialCooldown;
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
