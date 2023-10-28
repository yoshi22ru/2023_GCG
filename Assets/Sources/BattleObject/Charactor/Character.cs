using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Character : BattleObject
{
    public Animator animator;  // キャラクターのアニメーターコンポーネント
    private Character_State currentState; // 現在の状態
    private Transform characterPoint;
    [SerializeField] GameObject skill1;
    [SerializeField] GameObject skill2;
    [SerializeField] GameObject special;
    [SerializeField] Transform skill1Point;
    [SerializeField] Transform skill2Point;
    [SerializeField] Transform specialPoint;

    // キャラクターの状態を定義
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
        // アニメーターコンポーネントを取得
        animator = GetComponent<Animator>();
        // 最初の状態を設定
        SetState(Character_State.Idle);
    }

    private void FixedUpdate()
    {
        characterPoint = this.transform;
    }

    // キャラクターの状態を設定し、トリガーを発動するメソッド
    public void SetState(Character_State newState)
    {
        // 現在の状態を設定
        currentState = newState;

        // アニメーターのトリガーをリセット
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Damage");
        animator.ResetTrigger("Dead");
        animator.ResetTrigger("Skill1");
        animator.ResetTrigger("Skill2");
        animator.ResetTrigger("Special");

        // 新しい状態に応じてトリガーを設定
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
        // Damage状態の動作を実行
        Debug.Log(damage + "ダメージ喰らった");
    }

    protected virtual void Heal(int heal)
    {
        Debug.Log(heal + "回復");
    }

    protected virtual void Dead()
    {
        // Dead状態の動作を実行
        Debug.Log("死亡");
    }

    protected virtual void Skill1()
    {
        // Skill1状態の動作を実行
        Debug.Log("スキル1発動");
        Instantiate(skill1, skill1Point.position, transform.rotation);
    }

    protected virtual void Skill2()
    {
        // Skill2状態の動作を実行
        Debug.Log("スキル2発動");
        Instantiate(skill2, skill2Point.position, transform.rotation);
    }

    protected virtual void Special()
    {
        // Special状態の動作を実行
        Debug.Log("スペシャルスキル発動");
        Instantiate(special, specialPoint.position, transform.rotation, characterPoint);
    }
}


