using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sources.InGame.BattleObject.Character
{


    public class CharacterController : MonoBehaviour
    {
        private CharacterStatus characterStatus; // �L�����N�^�[�̃X�e�[�^�X

        private void Start()
        {
            // �L�����N�^�[�̃X�e�[�^�X���擾�܂��͏�����
            characterStatus = GetComponent<CharacterStatus>();
        }

        private void FixedUpdate()
        {
            if (characterStatus.IsDead)
            {
                // �L�����N�^�[�����S���Ă���ꍇ�͏������I��
                return;
            }

            // �L�[���͂Ɋ�Â��ď�ԑJ�ڂ𐧌�
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.D))
            {
                // WASD�L�[��������Ă���Ԃ�Run��ԂɑJ��
                characterStatus.UpdateStatus();
                //SetState(Character.Character_State.Run);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                // E�L�[���������ꍇ�ASkill1�𔭓�
                characterStatus.UseSkill1();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                // Q�L�[���������ꍇ�ASkill2�𔭓�
                characterStatus.UseSkill2();
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                // R�L�[���������ꍇ�ASpecial�𔭓�
                characterStatus.UseSpecial();
            }
            else
            {
                // �������͂���Ă��Ȃ��ꍇ��Idle��ԂɑJ��
                characterStatus.UpdateStatus();
                //SetState(Character.Character_State.Idle);
            }

            // �L�����N�^�[�̎��S������s��
        }
    }
}