using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using R3;
using Resources.Character;
using Sources.InGame.BattleObject;
using Sources.InGame.BattleObject.Castle;
using Sources.InGame.BattleObject.Character;
using UnityEngine;


public class GameManager : MonoBehaviourPunCallbacks
{
    #region variables

    private UserList _readUserList;
    List<Castle> catsles;
    private ReactiveProperty<BattleState> _currentState;
    public ReadOnlyReactiveProperty<BattleState> CurrentState => _currentState;
    // index is actor number
    [SerializeField] private CharaDataBase charaDataBase;
    [SerializeField] List<Transform> red_spawn_pos;
    [SerializeField] List<Transform> blue_spawn_pos;
    [SerializeField] private GameObject cameramanager;
    [SerializeField] float BattleTime;
    [SerializeField] CoolTimeView coolTimeView;
    [SerializeField] private CountDown CountDown;
    float current_time;
    public static GameManager manager;
    
    // FIXME
    public const float RespawnTime = 5.0f;
    private const float CountDownTime = 3.0f;

    #endregion

    private void Awake() {
        if (manager == null) {
            manager = this;
        } else {
            Destroy(this);
        }
        _currentState = new ReactiveProperty<BattleState>(BattleState.BeforeStart);
        _readUserList = new UserList(PhotonNetwork.PlayerList.Length);
    }

    private async void Start()
    {
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
        
        
        await UniTask.WaitUntil(_readUserList.CheckReady);
        StartCountDown();
        await UniTask.Delay(TimeSpan.FromSeconds(3));
        StartEvent();
    }

    private void StartCountDown()
    {
        Debug.Log("StartCountDown");
        CountDown.CountDownToStart(CountDownTime);
    }

    private void StartEvent() {
        _currentState.Value = BattleState.Battle;
        current_time = BattleTime;
    }

    private void FixedUpdate() {
        current_time -= Time.fixedDeltaTime;


        if (current_time <= 0.0f) {
            _currentState.Value = BattleState.Ended;

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

    public void ReadyOk(int actorNumber)
    {
        _readUserList.SetReady(actorNumber);
    }


    public enum BattleState : byte
    {
        BeforeStart,
        Battle,
        Ended,
    }

}
