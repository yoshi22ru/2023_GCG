using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SynchronizationManager : MonoBehaviourPunCallbacks
{
    #region  HashKey
    const string H_HP = "hp";
    const string H_Position = "pos";
    const string H_Rotation = "rote";
    const string H_Item = "item";
    #endregion


    enum CurrentScene {
        SelectCharactor,
        BattleScene,
    }
}
