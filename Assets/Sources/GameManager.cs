using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region variables
    List<Character> characters;
    List<Catsle> catsles;
    BattleState current_state;
    float current_time;

    #endregion

    private void Start() {
        current_state = BattleState.BeforeStart;
        
    }

    enum BattleState {
        BeforeStart,
        Battle,
        Ended,
    }
}
