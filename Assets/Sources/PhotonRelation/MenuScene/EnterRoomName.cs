using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace Sources.PhotonRelation.MenuScene
{
    public class EnterRoomName : MenuPanelBase
    {
        #region SettingName

        [SerializeField] InputField roomNameField;
        [SerializeField] Button submitButton;
        [SerializeField] Button cancelButton;

        private void SetNewName()
        {
            var newName = roomNameField.text;
            SetMyName(newName);
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
        private void SetMyName(string newName)
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LocalPlayer.NickName = newName;
                Debug.Log("NicName : " + newName);
            }
            else
            {
                Debug.Log("Not Connected");
            }
        }

    }
}