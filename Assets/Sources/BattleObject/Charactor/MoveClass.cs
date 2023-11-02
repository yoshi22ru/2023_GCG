using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.ComponentModel.Design;

public class MoveClass : MonoBehaviour
{
    private CharacterStatus characterStatus;
    private float speed;
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
        MovementControll();
        Movement();
    }

    void FixedUpdate()
    {
        RotateToMovingDirection();
    }
    void MovementControll()
    {
        //�΂߈ړ��Əc���̈ړ��𓯂����x�ɂ��邽�߂�Vector3��Normalize()����B
        moving = new Vector3(Input.GetAxisRaw("Vertical") ,0, -Input.GetAxisRaw("Horizontal"));
        moving.Normalize();
        moving = moving * speed;
    }

    public void RotateToMovingDirection()
    {
        Vector3 differenceDis = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(latestPos.x, 0, latestPos.z);
        latestPos = transform.position;
        //�ړ����ĂȂ��Ă���]���Ă��܂��̂ŁA���̋����ȏ�ړ��������]������
        if (Mathf.Abs(differenceDis.x) > 0.001f || Mathf.Abs(differenceDis.z) > 0.001f)
        {
            Quaternion rot = Quaternion.LookRotation(differenceDis);
            rot = Quaternion.Slerp(rb.transform.rotation, rot, 0.1f);
            this.transform.rotation = rot;
        }
    }

    void Movement()
    {
        rb.velocity = moving;
    }
}
