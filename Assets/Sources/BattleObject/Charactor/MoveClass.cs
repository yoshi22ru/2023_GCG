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
        //ŽÎ‚ßˆÚ“®‚Æc‰¡‚ÌˆÚ“®‚ð“¯‚¶‘¬“x‚É‚·‚é‚½‚ß‚ÉVector3‚ðNormalize()‚·‚éB
        moving = new Vector3(Input.GetAxisRaw("Vertical") ,0, -Input.GetAxisRaw("Horizontal"));
        moving.Normalize();
        moving = moving * speed;
    }

    public void RotateToMovingDirection()
    {
        Vector3 differenceDis = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(latestPos.x, 0, latestPos.z);
        latestPos = transform.position;
        //ˆÚ“®‚µ‚Ä‚È‚­‚Ä‚à‰ñ“]‚µ‚Ä‚µ‚Ü‚¤‚Ì‚ÅAˆê’è‚Ì‹——£ˆÈãˆÚ“®‚µ‚½‚ç‰ñ“]‚³‚¹‚é
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
