using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;



namespace Sources.OutGame.MenuScene
{
    public class SelectRandomOrPrivatePanel : MenuPanelBase
    {
        [SerializeField] private Button privateButton;
        [SerializeField] private Button randomButton;

        private void Start()
        {
            privateButton.onClick.AddListener(ShiftInputRoomName);
            randomButton.onClick.AddListener(RandomEnterRoom);
        }

        void ShiftInputRoomName()
        {
            Debug.Log("private room");
            
            Manager.ShiftPanel(MenuPanelDB.IdentPanel.SelectRoom);
        }

        void RandomEnterRoom()
        {
            Debug.Log("random enter room");
            PhotonNetwork.JoinRandomOrCreateRoom();
        }

        public override void OnJoinedRoom()
        {
            ButtonOn();
            Manager.ShiftPanel(MenuPanelDB.IdentPanel.SelectCharacter);
        }


        private void ButtonOff()
        {
            privateButton.interactable = false;
            randomButton.interactable = false;
        }

        private void ButtonOn()
        {
            privateButton.interactable = true;
            randomButton.interactable = true;
        }
    }
}