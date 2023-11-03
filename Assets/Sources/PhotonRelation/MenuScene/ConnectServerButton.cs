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
    void Start()
    {
        connect_button.onClick.AddListener(ConnectServer);
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

    public override void OnConnected()
    {
        Debug.Log("OnConnected");

        MenuPanelManager.instance.SetPanel(MenuPanelManager.Ident_Panel.SelectMatchingMode);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected");

        MenuPanelManager.instance.SetPanel(MenuPanelManager.Ident_Panel.SelectMatchingMode);
    }
}
