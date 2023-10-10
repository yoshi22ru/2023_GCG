using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour
{
    private int maxPlayer = 4;  int GetMaxPlayer() { return maxPlayer; }
    [SerializeField] private Text label = default;
    MatchMakingView matchMakingView;
    Button button;
    public string RoomName {get; private set;}

    public void Init(MatchMakingView parentView, int roomId) {
        matchMakingView = parentView;
        RoomName = $"Room{roomId}";

        button = GetComponent<Button>();
        button.interactable = false;
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick() {
        matchMakingView.OnJoiningRoom();

        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayer;
        PhotonNetwork.JoinOrCreateRoom(RoomName, roomOptions, TypedLobby.Default);
    }

    public void SetPlayerCount(int playerCount) {
        label.text = $"{RoomName}\n{playerCount} / {maxPlayer}";

        button.interactable = (playerCount < maxPlayer);
    }
}
