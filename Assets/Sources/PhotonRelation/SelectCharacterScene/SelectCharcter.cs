using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Pun;
using Photon.Realtime;
using Resources.Character;
using Sources.InGame.BattleObject;
using UnityEngine.Serialization;

public class SelectCharacter : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button up;
    [SerializeField] private Button down;
    [SerializeField] private Image charaSprite;
    [SerializeField] private Image teamColor;
    [SerializeField] CharaDataBase charaDataBase;
    private CharaData.Ident_Character _character = CharaData.Ident_Character.Bird;
    private int _actorNumber;
    private bool _decide;
    private BattleObject.Team _team;

    private void Start()
    {
        // Debug.Log("your actor number is : " + PhotonNetwork.LocalPlayer.ActorNumber);
        up.onClick.AddListener(ScrollUp);
        down.onClick.AddListener(ScrollDown);


        if (_actorNumber % 2 == 1)
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
        if (_actorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
        {
            return;
        }

        if ((_character - 1) <=
            (CharaData.Ident_Character)Enum.GetValues(typeof(CharaData.Ident_Character)).Cast<int>().Min())
        {
            // underflow
            SetData((CharaData.Ident_Character)Enum.GetValues(typeof(CharaData.Ident_Character)).Cast<int>().Max());
        }
        else
        {
            SetData(_character - 1);
        }
    }

    private void ScrollDown()
    {
        if (_actorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
        {
            return;
        }

        if ((_character + 1) >=
            (CharaData.Ident_Character)Enum.GetValues(typeof(CharaData.Ident_Character)).Cast<int>().Max())
        {
            // overflow
            SetData((CharaData.Ident_Character)Enum.GetValues(typeof(CharaData.Ident_Character)).Cast<int>().Min());
        }
        else
        {
            SetData(_character + 1);
        }
    }

    public void ChangeTeam()
    {
        if (_actorNumber != PhotonNetwork.LocalPlayer.ActorNumber) return;

        if (_team == BattleObject.Team.Blue)
        {
            SetTeam(BattleObject.Team.Red);
        }
        else
        {
            SetTeam(BattleObject.Team.Blue);
        }
    }

    #region receive event

    public void TurnDecision()
    {
        if (_actorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            SetDecision(!_decide);
        }
    }

    public void OnDecision()
    {
        if (_actorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
        {
            return;
        }

        SetDecision(true);
    }

    public void OffDecision()
    {
        if (_actorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
        {
            return;
        }

        SetDecision(false);
    }

    #endregion

    private void SetData(CharaData.Ident_Character identCharacter)
    {
        if (_decide) return;

        this._character = identCharacter;
        CharaData data = charaDataBase.character_data[(int)identCharacter];

        this.charaSprite.sprite = data.CharaSprite;

        if (_actorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            PhotonNetwork.LocalPlayer.SetCharacter((int)_character);
        }
    }

    private void SetTeam(BattleObject.Team team)
    {
        this._team = team;
        if (team == BattleObject.Team.Blue)
        {
            teamColor.color = Color.blue;
        }
        else
        {
            teamColor.color = Color.red;
        }

        if (_actorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            PhotonNetwork.LocalPlayer.SetTeam((int)team);
        }
    }

    private void SetDecision(bool decision)
    {
        if (!(_decide ^ decision))
        {
            return;
        }

        _decide = decision;
        if (decision)
        {
            charaSprite.color = Color.black;
        }
        else
        {
            charaSprite.color = Color.white;
        }

        if (_actorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
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
        // if update is not rerated this
        if (targetPlayer.ActorNumber != _actorNumber) return;
        // if updated properties are mine
        if (PhotonNetwork.LocalPlayer.ActorNumber == targetPlayer.ActorNumber) return;

        if (targetPlayer.TryGetCharacter(out var character))
        {
            SetData((CharaData.Ident_Character)character);
        }

        if (targetPlayer.TryGetTeam(out var team))
        {
            SetTeam((BattleObject.Team)team);
        }

        if (targetPlayer.TryGetDecision(out var isDecide))
        {
            SetDecision(isDecide);
        }
    }

    public void SetActorNum(int actorNumber)
    {
        this._actorNumber = actorNumber;
        Debug.Log("this actor num is : " + actorNumber);
    }

    public BattleObject.Team GetTeam()
    {
        return _team;
    }

    public bool GetDecision()
    {
        return _decide;
    }

    public CharaData.Ident_Character GetCharacter()
    {
        return _character;
    }

    public int GetActorNum()
    {
        return _actorNumber;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (_actorNumber != PhotonNetwork.LocalPlayer.ActorNumber) return;

        PhotonNetwork.LocalPlayer.SetCharacter((int)_character);
        PhotonNetwork.LocalPlayer.SetTeam((int)_team);
        PhotonNetwork.LocalPlayer.SetDecision(_decide);
    }
}
