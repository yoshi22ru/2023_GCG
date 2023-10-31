using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utility;

public class RandomMatchButton : MonoBehaviourPunCallbacks
{

    [SerializeField] private Button button;
    private void Start() {
        button.onClick.AddListener(ToConnectRoom);
    }
    public override void OnJoinedRoom()
    {
        SceneManager.LoadSceneAsync("CharacterSelect");
    }

    protected void ToConnectRoom()
    {
        PhotonUtility.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Guid guid = new Guid();
        PhotonUtility.CreateAndJoinRoom(guid.ToString().Substring(0,4), true);
    }
}
