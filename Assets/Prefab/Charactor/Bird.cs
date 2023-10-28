using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Character
{
    Transform characterPoint;
    [SerializeField] GameObject skill1;
    [SerializeField] GameObject skill2;
    [SerializeField] GameObject special;
    [SerializeField] Transform skill1Point;
    [SerializeField] Transform skill2Point;
    [SerializeField] Transform specialPoint;

    protected override void Skill1()
    {
        // Skill1状態の動作を実行
        Debug.Log("aaa");
        Instantiate(skill1, skill1Point.position, transform.rotation);
    }

    protected override void Skill2()
    {
        // Skill2状態の動作を実行
        Debug.Log("iii");
        Instantiate(skill2, skill2Point.position, transform.rotation);
    }

    protected override void Special()
    {
        // Special状態の動作を実行
        Debug.Log("uuu");
        Instantiate(special, specialPoint.position, transform.rotation, characterPoint);
    }
}
