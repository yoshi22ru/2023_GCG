using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace Sources.OutGame.MenuScene
{
    public class EnterRoomName : MenuPanelBase
    {
        #region SettingName

        [SerializeField] InputField roomNameField;
        [SerializeField] Button submitButton;
        [SerializeField] Button cancelButton;

        private void SetNewName()
        {
            ButtonOff();
            var newName = roomNameField.text;
            EnterRoom(newName);
        }

        private void CancelSetName()
        {
            roomNameField.text = "";
            Manager.ReturnPanel();
        }

        #endregion

        private void Start()
        {
            submitButton.onClick.AddListener(SetNewName);
            cancelButton.onClick.AddListener(CancelSetName);
        }
        private void EnterRoom(string newName)
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRoom(newName);
            }
            else
            {
                roomNameField.text = "Not Connected";
            }
        }

        public override void OnJoinedRoom()
        {
            ButtonOn();
            Manager.ShiftPanel(MenuPanelDB.IdentPanel.SelectCharacter);
        }


        private void ButtonOff()
        {
            submitButton.interactable = false;
            cancelButton.interactable = false;
        }

        private void ButtonOn()
        {
            submitButton.interactable = true;
            cancelButton.interactable = true;
        }
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            roomNameField.text = message;
            ButtonOn();
        }
        
    }
}