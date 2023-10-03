using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
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
