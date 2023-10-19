using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterController : Character
{
    private CharacterStatus characterStatus; // キャラクターのステータス

    private void Start()
    {
        // キャラクターのステータスを取得または初期化
        characterStatus = GetComponent<CharacterStatus>();
    }

    private void Update()
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
            characterStatus.UseSkill1();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // Qキーを押した場合、Skill2を発動
            characterStatus.UseSkill2();            
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            // Rキーを押した場合、Specialを発動
            characterStatus.UseSpecial();
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
}