using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Idle : StateMachineBehaviour
{
    // OnStateEnter�́A�J�ڂ��n�܂�A�X�e�[�g�}�V�������̏�Ԃ̕]�����J�n����Ƃ��ɌĂяo�����
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Idle�X�e�[�g�ɓ�����");
    }

    // OnStateUpdate�́AOnStateEnter�R�[���o�b�N��OnStateExit�R�[���o�b�N�̊Ԃ̊eUpdate�t���[���ŌĂ΂��
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Idle�X�e�[�g��Update���[�v������");
    }

    // OnStateExit�́A�J�ڂ��I�����A�X�e�[�g�}�V�������̏�Ԃ̕]�����I�������Ƃ��ɌĂяo�����
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Idle�X�e�[�g���甲����");
    }
}
