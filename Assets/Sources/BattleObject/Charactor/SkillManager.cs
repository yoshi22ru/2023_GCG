using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public float skillSpeed;
    public float leftTime;
    private float currentTime;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * skillSpeed;
    }

    void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        if (currentTime > leftTime)
        {
            Destroy(gameObject);
            currentTime = 0;
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        // Destroy(gameObject);
    }
}
