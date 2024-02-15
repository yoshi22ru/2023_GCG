using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectMatchStilePanel : MonoBehaviourPunCallbacks
{
    MenuPanelManager panelManager;
    [SerializeField] private Button random_match_button;
    [SerializeField] private Button private_match_button;

    private void Start() {
        panelManager = MenuPanelManager.instance;
        random_match_button.onClick.AddListener(EnterRandomRoom);
        private_match_button.onClick.AddListener(ShiftInputRoomNamePanel);
    }

    void EnterRandomRoom() {
        Utility.PhotonUtility.JoinRandomRoom();
    }

    void ShiftInputRoomNamePanel() {
        panelManager.SetPanel(MenuPanelManager.Ident_Panel.InputRoomName);
    }

    public override void OnJoinedRoom() {
        StartCoroutine(Utility.PhotonUtility.LoadYourAsyncScene("SelectCharacter"));
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        string roomName = "";
        for (int i = 0; i < 5; ++i) {
            var buf = (char)Random.Range('a', 'Z');
            roomName += buf;
        }
        Utility.PhotonUtility.CreateAndJoinRoom(roomName, true);
    }
}
