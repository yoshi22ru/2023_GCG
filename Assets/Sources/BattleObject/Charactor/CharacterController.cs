using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterController : Character
{
    private CharacterStatus characterStatus; // �L�����N�^�[�̃X�e�[�^�X

    private void Start()
    {
        // �L�����N�^�[�̃X�e�[�^�X���擾�܂��͏�����
        characterStatus = GetComponent<CharacterStatus>();
    }

    private void Update()
    {
        if (characterStatus.IsDead)
        {
            // �L�����N�^�[�����S���Ă���ꍇ�͏������I��
            return;
        }

        // �L�[���͂Ɋ�Â��ď�ԑJ�ڂ𐧌�
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            // WASD�L�[��������Ă���Ԃ�Run��ԂɑJ��
            characterStatus.UpdateStatus();
            SetState(Character.Character_State.Run);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // E�L�[���������ꍇ�ASkill1�𔭓�
            characterStatus.UseSkill1();
            SetState(Character.Character_State.Skill1);
            
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // Q�L�[���������ꍇ�ASkill2�𔭓�
            characterStatus.UseSkill2();
            SetState(Character.Character_State.Skill2);
            
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            // R�L�[���������ꍇ�ASpecial�𔭓�
            characterStatus.UseSpecial();
            SetState(Character.Character_State.Special);
        }
        else
        {
            // �������͂���Ă��Ȃ��ꍇ��Idle��ԂɑJ��
            characterStatus.UpdateStatus();
            SetState(Character.Character_State.Idle);
        }

        // �L�����N�^�[�̎��S������s��
        characterStatus.CheckDeath();
    }
}