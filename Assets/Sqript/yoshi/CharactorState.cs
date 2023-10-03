using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    public enum Character
    {
        None,
        Idle,
        Attack,
        Damage,
        Dead,
        Skill1,
        Skill2,
        Special,
    }
}
