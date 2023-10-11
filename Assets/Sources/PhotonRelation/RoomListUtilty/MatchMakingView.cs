using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MatchMakingView : MonoBehaviourPunCallbacks
{
    RoomList roomList = new RoomList();
    List<RoomButton> roomButtonList = new List<RoomButton>();
    CanvasGroup canvasGroup;

    private void Start() {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;

        int roomId = 1;
        foreach (Transform child in transform) {
            if (child.TryGetComponent<RoomButton>(out var roomButton)) {
                roomButton.Init(this, roomId++);
                roomButtonList.Add(roomButton);
            }
        }
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        canvasGroup.interactable = true;
    }

    public override void OnRoomListUpdate(List<RoomInfo> changeRoomList)
    {
        base.OnRoomListUpdate(changeRoomList);

        roomList.Update(changeRoomList);
        foreach (var roomButton in roomButtonList) {
            if (roomList.TryGetRoomInfo(roomButton.RoomName, out var roomInfo)) {
                roomButton.SetPlayerCount(roomInfo.PlayerCount);
            } else {
                roomButton.SetPlayerCount(0);
            }
        }
    }

    public void OnJoiningRoom() {
        canvasGroup.interactable = false;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        gameObject.SetActive(false);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);

        canvasGroup.interactable = true;
    }
}
