using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Pun;
using Photon.Realtime;

public class SelectCharctor : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button up;
    [SerializeField] private Button down;
    [SerializeField] private Sprite chara_sprite;
    [SerializeField] CharaDataBase charaDataBase;
    CharaData.Ident_Charactor _Charactor;
    [SerializeField] private int actor_number;

    private void Start()
    {
        if (actor_number == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            up.onClick.AddListener(ScrollUp);
            down.onClick.AddListener(ScrollDown);
        }

        _Charactor = (CharaData.Ident_Charactor)Enum.GetValues(typeof(CharaData.Ident_Charactor)).Cast<int>().Min();
    }

    private void ScrollUp()
    {
        --_Charactor;
        if (_Charactor <= (CharaData.Ident_Charactor)Enum.GetValues(typeof(CharaData.Ident_Charactor)).Cast<int>().Min())
        {
            // underflow
            _Charactor = (CharaData.Ident_Charactor)Enum.GetValues(typeof(CharaData.Ident_Charactor)).Cast<int>().Max();
        }

        SetData();
    }
    private void ScrollDown()
    {
        ++_Charactor;
        if (_Charactor >= (CharaData.Ident_Charactor)Enum.GetValues(typeof(CharaData.Ident_Charactor)).Cast<int>().Max())
        {
            // overflow
            _Charactor = (CharaData.Ident_Charactor)Enum.GetValues(typeof(CharaData.Ident_Charactor)).Cast<int>().Min();
        }

        SetData();
    }

    private void SetData()
    {
        CharaData data = charaDataBase.charadata[(int)_Charactor];

        chara_sprite = data.CharaSprite;
        // TODO!
        PhotonNetwork.LocalPlayer.SetCharacter((int)_Charactor);
    }

    private void LateUpdate()
    {
        PhotonNetwork.LocalPlayer.SendPlayerProperties();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer.ActorNumber == actor_number)
        {
            _Charactor = (CharaData.Ident_Charactor)targetPlayer.GetCharacter();
        }
    }
}
