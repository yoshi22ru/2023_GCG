using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Pun;
using Photon.Realtime;

public class SelectCharacter : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button up;
    [SerializeField] private Button down;
    [SerializeField] private Image chara_sprite;
    [SerializeField] private Image team_color;
    [SerializeField] CharaDataBase charaDataBase;
    CharaData.Ident_Character _Character;
    int actor_number;
    bool decide;
    BattleObject.Team team;

    private void Start()
    {
        // Debug.Log("your actor number is : " + PhotonNetwork.LocalPlayer.ActorNumber);
        if (actor_number == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            up.onClick.AddListener(ScrollUp);
            down.onClick.AddListener(ScrollDown);
        }

        if (actor_number % 2 == 1)
        {
            SetTeam(BattleObject.Team.Red);
        }
        else
        {
            SetTeam(BattleObject.Team.Blue);
        }

        SetDecision(false);
        SetData((CharaData.Ident_Character)Enum.GetValues(typeof(CharaData.Ident_Character)).Cast<int>().Min());
    }

    private void ScrollUp()
    {
        if ((_Character - 1) <= (CharaData.Ident_Character)Enum.GetValues(typeof(CharaData.Ident_Character)).Cast<int>().Min())
        {
            // underflow
            SetData((CharaData.Ident_Character)Enum.GetValues(typeof(CharaData.Ident_Character)).Cast<int>().Max());
        }
        else
        {
            SetData(_Character - 1);
        }
    }
    private void ScrollDown()
    {
        if ((_Character + 1) >= (CharaData.Ident_Character)Enum.GetValues(typeof(CharaData.Ident_Character)).Cast<int>().Max())
        {
            // overflow
            SetData((CharaData.Ident_Character)Enum.GetValues(typeof(CharaData.Ident_Character)).Cast<int>().Min());
        }
        else
        {
            SetData(_Character + 1);
        }
    }

    public void ChangeTeam()
    {
        if (actor_number != PhotonNetwork.LocalPlayer.ActorNumber) return;

        if (team == BattleObject.Team.Blue)
        {
            SetTeam(BattleObject.Team.Red);
        }
        else
        {
            SetTeam(BattleObject.Team.Blue);
        }
    }

    private void SetData(CharaData.Ident_Character ident_Character)
    {
        this._Character = ident_Character;
        CharaData data = charaDataBase.charadata[(int)ident_Character];

        this.chara_sprite.sprite = data.CharaSprite;

        if (actor_number == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            VariableManager.character = ident_Character;
            PhotonNetwork.LocalPlayer.SetTeam((int)team);
        }
    }

    private void SetTeam(BattleObject.Team team)
    {
        Debug.Log("num" + actor_number + " is : " + team);
        this.team = team;
        if (team == BattleObject.Team.Blue)
        {
            team_color.color = Color.blue;
        }
        else
        {
            team_color.color = Color.red;
        }

        if (actor_number == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            VariableManager.my_team = team;
            PhotonNetwork.LocalPlayer.SetCharacter((int)_Character);
        }
    }

    public void SetDecision(bool decision) {
        if (!(decide^decision)) {
            return;
        }
        decide = decision;
        if (decision) {
            chara_sprite.color = Color.black;
        } else {
            chara_sprite.color = Color.white;
        }
        if (actor_number == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            PhotonNetwork.LocalPlayer.SetDecision(decision);
        }
    }

    private void LateUpdate()
    {
        PhotonNetwork.LocalPlayer.SendPlayerProperties();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer.ActorNumber != actor_number) return;
        if (PhotonNetwork.LocalPlayer.ActorNumber == targetPlayer.ActorNumber) return;

        if (targetPlayer.TryGetCharacter(out var character))
        {
            SetData((CharaData.Ident_Character)character);
        }

        if (targetPlayer.TryGetTeam(out var team))
        {
            SetTeam((BattleObject.Team)team);
        }

        if (targetPlayer.TryGetDecision(out var is_decide)) {
            SetDecision(is_decide);
        }
    }

    public void SetActorNum(int actor_number)
    {
        this.actor_number = actor_number;
        Debug.Log("this actor num is : " + actor_number);
    }

    public BattleObject.Team GetTeam()
    {
        return team;
    }

    public bool GetDecision() {
        return decide;
    }

    public CharaData.Ident_Character GetCharacter() {
        return _Character;
    }

    public int GetActorNum() {
        return actor_number;
    }

    public void TurnDecision() {
        if (actor_number == PhotonNetwork.LocalPlayer.ActorNumber) {
            SetDecision(!decide);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PhotonNetwork.LocalPlayer.SetCharacter((int) _Character);
        PhotonNetwork.LocalPlayer.SetTeam((int) team);
        PhotonNetwork.LocalPlayer.SetDecision(decide);
    }
}
