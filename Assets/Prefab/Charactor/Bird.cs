using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bird : Character
{
    [SerializeField] GameObject skill1;
    [SerializeField] GameObject skill2;
    [SerializeField] GameObject special;
    [SerializeField] Transform skill1Point;
    [SerializeField] Transform skill2Point;
    [SerializeField] Transform specialPoint;
    [SerializeField]
    private int Skill1Damage = 15;
    [SerializeField]
    private int Skill2Damage = 30;
    [SerializeField]
    private float SpecialBuf = 3f;
    [SerializeField] AudioClip skill1SE;
    [SerializeField] AudioClip skill2SE;
    [SerializeField] AudioClip specialSE;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected override void Skill1()
    {
        Instantiate(skill1, skill1Point.position, transform.rotation);
        base.Skill1();
        audioSource.PlayOneShot(skill1SE);
    }

    protected override void Skill2()
    {
        Instantiate(skill2, skill2Point.position, transform.rotation);
        base.Skill2();
        audioSource.PlayOneShot(skill2SE);
    }

    protected override void Special()
    {
        Instantiate(special, specialPoint.position, transform.rotation);
        base.Special();
        audioSource.PlayOneShot(specialSE);
    }
}
