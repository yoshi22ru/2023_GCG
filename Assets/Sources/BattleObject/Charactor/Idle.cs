using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Idle : StateMachineBehaviour
{
    // OnStateEnterは、遷移が始まり、ステートマシンがこの状態の評価を開始するときに呼び出される
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Idleステートに入った");
    }

    // OnStateUpdateは、OnStateEnterコールバックとOnStateExitコールバックの間の各Updateフレームで呼ばれる
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("IdleステートでUpdateループ処理中");
    }

    // OnStateExitは、遷移が終了し、ステートマシンがこの状態の評価を終了したときに呼び出される
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Idleステートから抜けた");
    }
}
