using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class ConnectServerButton : MonoBehaviourPunCallbacks
{
    public bool offlineMode;
    [SerializeField] private Button connect_button;
    void Start()
    {
        connect_button.onClick.AddListener(ConnectServer);
    }

    void ConnectServer()
    {
        connect_button.interactable = false;

        if (offlineMode)
        {
            PhotonNetwork.OfflineMode = true;
        }
        
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
        connect_button.interactable = true;
        MenuPanelManager.instance.SetPanel(MenuPanelManager.Ident_Panel.SelectMatchingMode);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected");
        connect_button.interactable = true;
        MenuPanelManager.instance.SetPanel(MenuPanelManager.Ident_Panel.SelectMatchingMode);

    }
}
