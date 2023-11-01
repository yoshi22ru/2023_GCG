using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Character
{
    [SerializeField] GameObject skill1;
    [SerializeField] GameObject skill2;
    [SerializeField] GameObject special;
    [SerializeField] Transform skill1Point;
    [SerializeField] Transform skill2Point;
    [SerializeField] Transform specialPoint;

    protected override void Skill1()
    {
        Instantiate(skill1, skill1Point.position, transform.rotation);
        base.Skill1();
    }

    protected override void Skill2()
    {
        Instantiate(skill2, skill2Point.position, transform.rotation);
        base.Skill2();
    }

    protected override void Special()
    {
        Instantiate(special, specialPoint.position, transform.rotation);
        base.Special();
    }
}