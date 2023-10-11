using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : BattleObject
{ 
    public Animator animator;  // キャラクターのアニメーターコンポーネント

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

    private Character_State currentState; // 現在の状態

    private void Start()
    {
        // アニメーターコンポーネントを取得
        animator = GetComponent<Animator>();
        // 最初の状態を設定
        SetState(Character_State.Idle);
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

    // 各状態に対応する動作を実行するメソッド
    private void Idle()
    {
        // Idle状態の動作を実行
    }

    private void Run()
    {
        // Run状態の動作を実行
    }

    private void Damage()
    {
        // Damage状態の動作を実行
    }

    private void Dead()
    {
        // Dead状態の動作を実行
    }

    private void Skill1()
    {
        // Skill1状態の動作を実行
    }

    private void Skill2()
    {
        // Skill2状態の動作を実行
    }

    private void Special()
    {
        // Special状態の動作を実行
    }
}

