using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.ComponentModel.Design;

public class MoveClass : MonoBehaviour
{
    private CharacterStatus characterStatus;
    private float speed;
    private Vector3 characterPos;
    private float x;
    private float z;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterStatus = GetComponent<CharacterStatus>();
        speed = characterStatus.MoveSpeed;

    }

    private void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        characterPos = new Vector3(x,0,z);

        if (characterPos.magnitude > speed)
        {
            characterPos.Normalize();
        }
        else if (characterPos.magnitude > 0.1f) 
        {
            transform.rotation = Quaternion.LookRotation(characterPos);
        }
        rb.AddForce(new Vector3(x * speed, 0, z * speed) * 100);
        rb.AddForce(-GetComponent<Rigidbody>().velocity / Time.fixedDeltaTime);
    }
}
