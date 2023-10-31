using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class ConnectServerButton : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button connect_button;
    [SerializeField] private GameObject select_match_mode_panel;
    void Start()
    {
        connect_button.onClick.AddListener(ConnectServer);
        select_match_mode_panel.SetActive(false);
    }

    void ConnectServer()
    {
        connect_button.interactable = false;

        if (PhotonNetwork.IsConnected == false)
        {
            PhotonNetwork.GameVersion = "v1.0";
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("Already connected");
        }
    }

    public override void OnConnectedToMaster() {
        Debug.Log("connected");

        select_match_mode_panel.SetActive(true);
    }
}
