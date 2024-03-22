using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Resources.Character;
using Sources.InGame.BattleObject;
using Sources.InGame.BattleObject.Castle;
using Sources.InGame.BattleObject.Character;
using UnityEngine;


public class GameManager : MonoBehaviourPunCallbacks
{
    #region variables
    List<Castle> catsles;
    public BattleState current_state;
    // index is actor number
    [SerializeField] private CharaDataBase charaDataBase;
    [SerializeField] List<Transform> red_spawn_pos;
    [SerializeField] List<Transform> blue_spawn_pos;
    [SerializeField] private GameObject cameramanager;
    [SerializeField] float BattleTime;
    [SerializeField] CoolTimeView coolTimeView;
    float current_time;
    public static GameManager manager;
    
    public const float RespawnTime = 5.0f;

    #endregion

    private void Awake() {
        if (manager == null) {
            manager = this;
        } else {
            Destroy(this);
        }
    }

    private void Start()
    {
        current_state = BattleState.BeforeStart;
        CharaData chara =
         charaDataBase.characterData[(int)VariableManager.GetCharacterByActorNum(PhotonNetwork.LocalPlayer.ActorNumber)];

        GameObject obj;
        if (VariableManager.GetTeamByActorNumber(PhotonNetwork.LocalPlayer.ActorNumber) == BattleObject.Team.Blue) {
            obj = PhotonNetwork.Instantiate(chara.CharaName.ToString(),
             blue_spawn_pos[VariableManager.GetIndex(VariableManager.GetTeamByActorNumber(PhotonNetwork.LocalPlayer.ActorNumber), PhotonNetwork.LocalPlayer.ActorNumber)].position,
             Quaternion.Euler(0.0f, -90.0f, 0.0f));
        } else {
            obj = PhotonNetwork.Instantiate(chara.CharaName.ToString(),
             red_spawn_pos[VariableManager.GetIndex(VariableManager.GetTeamByActorNumber(PhotonNetwork.LocalPlayer.ActorNumber), PhotonNetwork.LocalPlayer.ActorNumber)].position,
             Quaternion.Euler(0.0f, 90.0f, 0.0f));
        }
        var tmp = obj.GetComponent<Character>();
        coolTimeView.SetStatus(obj.GetComponent<CharacterStatus>());
        
        // FIXME
        StartEvent();
    }

    public void StartEvent() {
        current_state = BattleState.Battle;
        current_time = BattleTime;
    }

    private void FixedUpdate() {
        current_time -= Time.fixedDeltaTime;


        if (current_time <= 0.0f) {
            current_state = BattleState.Ended;

            // TODO! end game
        }
    }

    public void ReSetPosition(Character character)
    {
        switch (character.GetTeam())
        {
            case BattleObject.Team.Blue:
                character.gameObject.transform.position =
                    blue_spawn_pos[
                        VariableManager.GetIndex(
                            VariableManager.GetTeamByActorNumber(character.photonView.OwnerActorNr),
                            character.photonView.OwnerActorNr)].position;
                break;
            case BattleObject.Team.Red:
                character.gameObject.transform.position =
                    red_spawn_pos[
                        VariableManager.GetIndex(
                            VariableManager.GetTeamByActorNumber(character.photonView.OwnerActorNr),
                            character.photonView.OwnerActorNr)].position;
                break;
        }
    }


    public enum BattleState
    {
        BeforeStart,
        Battle,
        Ended,
    }

}
