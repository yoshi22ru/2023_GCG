using System.IO;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    #region variables
    List<Catsle> catsles;
    BattleState current_state;
    // index is actor number
    List<PlayerInfomations> characters;
    [SerializeField] private CharaDataBase charaDataBase;
    [SerializeField] List<Transform> red_spawn_pos;
    [SerializeField] List<Transform> blue_spawn_pos;
    [SerializeField] private
    GameObject cameramanager;
    public static GameManager manager;

    #endregion

    private void Start()
    {
        current_state = BattleState.BeforeStart;
        CharaData chara =
         charaDataBase.charadata[(int)VariableManager.character];

        GameObject obj;
        Debug.Log("object : " + chara.name + "\n");
        Debug.Log("position : " + VariableManager.my_team +
        PhotonNetwork.LocalPlayer.ActorNumber + "\n");
        if (VariableManager.my_team == BattleObject.Team.Blue) {
            obj = PhotonNetwork.Instantiate(chara.CharaName,
             blue_spawn_pos[VariableManager.GetIndex(VariableManager.my_team, PhotonNetwork.LocalPlayer.ActorNumber)].position,
             Quaternion.Euler(0.0f, -90.0f, 0.0f));
        } else {
            obj = PhotonNetwork.Instantiate(chara.CharaName,
             red_spawn_pos[VariableManager.GetIndex(VariableManager.my_team, PhotonNetwork.LocalPlayer.ActorNumber)].position,
             Quaternion.Euler(0.0f, 90.0f, 0.0f));
        }
        var tmp = obj.GetComponent<Character>();
        Instantiate(cameramanager, obj.transform).transform.parent = obj.transform;

        Debug.Log("cameramanager set");

        CameraManager.instance.myCharacter = obj;
        CameraManager.instance.Initialize();

        Debug.Log("tmp" + tmp);
        photonView.RPC(nameof(tmp.Initialize), RpcTarget.All, VariableManager.my_team);
    }


    enum BattleState
    {
        BeforeStart,
        Battle,
        Ended,
    }

    public struct PlayerInfomations {
        public Character character;
        public CharacterStatus status;
        public int actor_number;
        public BattleObject.Team team;
        public PlayerInfomations(Character character, CharacterStatus characterStatus,
         int actor_number, BattleObject.Team team) {
            this.character = character;
            this.status = characterStatus;
            this.actor_number = actor_number;
            this.team = team;
        }
    }

    public void AddPlayer(Character character, CharacterStatus characterStatus,
         int actor_number, BattleObject.Team team) {
        characters.Add(new PlayerInfomations(
            character, characterStatus,
            actor_number, team
        ));
    }
}
