using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] int power;
    [SerializeField] float speed;
    CharacterState state;
    PlayerController playerController;

    void OnDamage(int damage)
    {
        hp -= damage;
    }

    void ReserveInput()
    {

    }

    void Skill1()
    {

    }
    void Skill2()
    {

    }

    void Special()
    {

    }

    void SetAnim()
    {

    }
}
