using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;


namespace Sources.PhotonRelation.MenuScene
{
    public class SelectOnlineOrOfflineModePanel : MenuPanelBase
    {
        [SerializeField] private Button onlineButton;
        [SerializeField] private Button offlineButton;

        private void Start()
        {
            onlineButton.onClick.AddListener(ToOnline);
            offlineButton.onClick.AddListener(ToOffline);
        }

        private void ToOnline()
        {
            onlineButton.interactable = false;

            if (PhotonNetwork.IsConnected)
            {
                Manager.ShiftPanel(MenuPanelDB.IdentPanel.SelectRoomByRandomOrSelect);
            }
            else
            {
                PhotonNetwork.GameVersion = "v1.0";
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        private void ToOffline()
        {
            offlineButton.interactable = false;

            if (PhotonNetwork.IsConnected)
            {
                Manager.ShiftPanel(MenuPanelDB.IdentPanel.SelectRoomByRandomOrSelect);
            }
            else
            {
                PhotonNetwork.GameVersion = "v1.0";
                PhotonNetwork.OfflineMode = true;
            }
        }

        // public override void OnConnected()
        // {
        //     onlineButton.interactable = true;
        //     offlineButton.interactable = true;
        //     Manager.ShiftPanel(MenuPanelDB.IdentPanel.SelectRoomByRandomOrSelect);
        // }

        public override void OnConnectedToMaster()
        {
            onlineButton.interactable = true;
            offlineButton.interactable = true;
            Manager.ShiftPanel(MenuPanelDB.IdentPanel.SelectRoomByRandomOrSelect);
        }
    }
}