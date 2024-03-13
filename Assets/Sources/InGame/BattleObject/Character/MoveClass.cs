using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.ComponentModel.Design;
using Sources.InGame.BattleObject.Character;

public class MoveClass
{
    private CharacterStatus characterStatus;
    private Vector3 moving, latestPos;
    Rigidbody rb;
    private Transform _myTrans;

    public MoveClass(Transform transform, Rigidbody rb, CharacterStatus characterStatus)
    {
        this._myTrans = transform;
        this.rb = rb;
        this.characterStatus = characterStatus;
    }

    public void Move(Vector2 inputVector2)
    {
        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * inputVector2.y + Camera.main.transform.right * inputVector2.x;

        // �ړ������ɃX�s�[�h���|����B
        rb.velocity = moveForward * characterStatus.MoveSpeed * 1.5f;

        // �L�����N�^�[�̌�����i�s������
        if (moveForward != Vector3.zero && CountDown.instance.isCountFinish == true)
        {
            Quaternion quaternion = Quaternion.LookRotation(moveForward);
            _myTrans.rotation =
                Quaternion.Slerp(_myTrans.rotation, quaternion, characterStatus.MoveSpeed * 0.005f);
        }
    }
}
