using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : BattleObject
{
    [SerializeField] int hp;
    [SerializeField] int power;
    [SerializeField] float speed;
    Character_State state;
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

    
    public enum Character_State
    {
        None,
        Idle,
        Run,
        Attack,
        Damage,
        Dead,
        Skill1,
        Skill2,
        Special,
    }
}
