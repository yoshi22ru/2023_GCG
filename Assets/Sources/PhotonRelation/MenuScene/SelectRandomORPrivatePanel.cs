using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SelectRandomORPrivatePanel : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button private_button;
    [SerializeField] private Button random_button;
    
    private void Start() {
        private_button.onClick.AddListener(ShiftInputRoomName);
        random_button.onClick.AddListener(RandomEnterRoom);
    }

    void ShiftInputRoomName() {
        Debug.Log("private room");
        PanelManager.instance.SetPanel(PanelManager.Ident_Panel.InputRoomName);
    }
    void RandomEnterRoom() {
        Debug.Log("random enter room");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom() {
        Debug.Log("Joined");

        SceneManager.LoadScene("CharacterSelect");
        Debug.Log("loaded");
    }

    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        string room_name = "";
        char buf;
        for (int i = 0; i < 5; ++i) {
            buf = (char) Random.Range('a', 'Z');
            room_name += buf;
        }
        Debug.Log("try to make " + room_name);
        Utility.PhotonUtility.CreateAndJoinRoom(room_name, true);
    }

}
