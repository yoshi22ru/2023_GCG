using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class main : Utility.PhotonUtility
{
    /// <summary>
    /// Main
    /// </summary>
    private void Start() {
        Utility.PhotonUtility.Connect("v1.0");

        Utility.PhotonUtility.CreateAndJoinRoom("string1", true);
    }

    #region Variables

    List<RoomInfo> room_list;

    #endregion

    #region CallBacks

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate");
        room_list = roomList;
        foreach (RoomInfo room in roomList) {
            Debug.Log(room);
        }
    }

    #endregion
}
