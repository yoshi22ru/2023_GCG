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
    List<PlayerInformation> characters;
    [SerializeField] private CharaDataBase charaDataBase;
    [SerializeField] List<Transform> red_spawn_pos;
    [SerializeField] List<Transform> blue_spawn_pos;
    [SerializeField] private GameObject cameramanager;
    [SerializeField] float BattleTime;
    [SerializeField] CoolTimeView coolTimeView;
    float current_time;
    public static GameManager manager;

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

            // TODO!
        }
    }


    public enum BattleState
    {
        BeforeStart,
        Battle,
        Ended,
    }

    public struct PlayerInformation {
        public Character character;
        public CharacterStatus status;
        public int actor_number;
        public BattleObject.Team team;
        public PlayerInformation(Character character, CharacterStatus characterStatus,
         int actorNumber, BattleObject.Team team) {
            this.character = character;
            this.status = characterStatus;
            this.actor_number = actorNumber;
            this.team = team;
        }
    }

    public void AddPlayer(Character character, CharacterStatus characterStatus,
         int actor_number, BattleObject.Team team) {
        characters.Add(new PlayerInformation(
            character, characterStatus,
            actor_number, team
        ));
    }
}
