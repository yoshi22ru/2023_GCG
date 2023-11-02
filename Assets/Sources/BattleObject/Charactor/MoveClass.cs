using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.ComponentModel.Design;

public class MoveClass : MonoBehaviour
{
    private CharacterStatus characterStatus;
    private float speed;
    float inputHorizontal;
    float inputVertical;
    private Vector3 moving, latestPos;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterStatus = GetComponent<CharacterStatus>();
        speed = characterStatus.MoveSpeed;

    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

    }

    void FixedUpdate()
    {
        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

        // �ړ������ɃX�s�[�h���|����B
        rb.velocity = moveForward * speed * 1.5f;

        // �L�����N�^�[�̌�����i�s������
        if (moveForward != Vector3.zero)
        {
            Quaternion quaternion = Quaternion.LookRotation(moveForward);
            this.transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, speed * 0.005f);
        }
    }
}
