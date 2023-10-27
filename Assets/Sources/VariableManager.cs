using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class VariableManager : MonoBehaviourPunCallbacks
{
    static List<CharaData.Ident_Charactor> player_charactors {
        get;
    }

    public static void AddCharactor(CharaData.Ident_Charactor ident_Charactor) {
        player_charactors.Add(ident_Charactor);
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        CharaData.Ident_Charactor ident_Charactor ;
    }
}
