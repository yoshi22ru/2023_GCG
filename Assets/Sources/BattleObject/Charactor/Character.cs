using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class Character : BattleObject
{
    public Animator animator;  // キャラクターのアニメーターコンポーネント
    private Character_State currentState; // 現在の状態
    private CharacterStatus characterStatus; // キャラクターのステータス
    // Transform characterPoint;
    //[SerializeField] GameObject skill1;
    //[SerializeField] GameObject skill2;
    //[SerializeField] GameObject special;
    //[SerializeField] Transform skill1Point;
    //[SerializeField] Transform skill2Point;
    //[SerializeField] Transform specialPoint;

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
        // キャラクターのステータスを取得または初期化
        characterStatus = GetComponent<CharacterStatus>();
        // アニメーターコンポーネントを取得
        animator = GetComponent<Animator>();
        // 最初の状態を設定
        SetState(Character_State.Idle);
    }

    private void FixedUpdate()
    {
        if (characterStatus.IsDead)
        {
            // キャラクターが死亡している場合は処理を終了
            return;
        }

        // キー入力に基づいて状態遷移を制御
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            // WASDキーが押されている間はRun状態に遷移
            characterStatus.UpdateStatus();
            SetState(Character.Character_State.Run);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // Eキーを押した場合、Skill1を発動
            Skill1();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // Qキーを押した場合、Skill2を発動
            Skill2();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            // Rキーを押した場合、Specialを発動
            Special();
        }
        else
        {
            // 何も入力されていない場合はIdle状態に遷移
            characterStatus.UpdateStatus();
            SetState(Character.Character_State.Idle);
        }

        // キャラクターの死亡判定を行う
        characterStatus.CheckDeath();
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
        //Instantiate(skill1, skill1Point.position, transform.rotation);
        characterStatus.UseSkill1();
    }

    protected virtual void Skill2()
    {
        // Skill2状態の動作を実行
        Debug.Log("スキル2発動");
        //Instantiate(skill2, skill2Point.position, transform.rotation);
        characterStatus.UseSkill2();
    }

    protected virtual void Special()
    {
        // Special状態の動作を実行
        Debug.Log("スペシャルスキル発動");
        //Instantiate(special, specialPoint.position, transform.rotation, characterPoint);
        characterStatus.UseSpecial();
    }
}


