using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region variables
    List<Character> characters;
    List<Catsle> catsles;
    BattleState current_state;
    float current_time;
    [SerializeField] private CharaDataBase charaDataBase;
    [SerializeField] private List<Vector3> spawn_position;

    #endregion

    private void Start() {
        current_state = BattleState.BeforeStart;
        CharaData player_chara = 
            charaDataBase.charadata[(int) VariableManager.player_charactor];
        
        Instantiate(player_chara.CharactorPrefab,
         spawn_position[PhotonNetwork.LocalPlayer.ActorNumber],
         Quaternion.identity);
        
        
    }

    enum BattleState {
        BeforeStart,
        Battle,
        Ended,
    }
}
