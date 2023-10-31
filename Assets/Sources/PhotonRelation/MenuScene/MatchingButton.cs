using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchingButton : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button button;
    public readonly string scene_name;
    private void Start() {
        button.interactable = false;
        button.onClick.AddListener(ToConnectRoom);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.IsMessageQueueRunning = false;

        SceneManager.LoadSceneAsync(scene_name);
    }

    protected virtual void ToConnectRoom() {

    }

    public override void OnConnected()
    {
        button.interactable = true;
    }
}
