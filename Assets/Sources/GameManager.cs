using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    #region variables
    List<Catsle> catsles;
    BattleState current_state;
    float current_time;
    // index is actor number
    List<Dictionary<BattleObject.Team, GameObject>> characters;
    [SerializeField] private CharaDataBase charaDataBase;
    [SerializeField] List<Vector3> red_spawn_pos;
    [SerializeField] List<Vector3> blue_spawn_pos;

    #endregion

    private void Start() {
        current_state = BattleState.BeforeStart;
        CharaData chara =
         charaDataBase.charadata[(int) VariableManager.character];

        if (VariableManager.my_team == BattleObject.Team.Blue) {
        }
    }

    enum BattleState {
        BeforeStart,
        Battle,
        Ended,
    }
}
