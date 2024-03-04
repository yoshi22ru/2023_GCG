using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace Sources.PhotonRelation.MenuScene
{


    public class SelectMatchStilePanel : MenuPanelBase
    {
        [SerializeField] private Button randomMatchButton;
        [SerializeField] private Button privateMatchButton;

        private void Start()
        {
            randomMatchButton.onClick.AddListener(EnterRandomRoom);
            privateMatchButton.onClick.AddListener(ShiftInputRoomNamePanel);
        }

        void EnterRandomRoom()
        {
            Utility.PhotonUtility.JoinRandomRoom();
        }

        void ShiftInputRoomNamePanel()
        {
            Manager.ShiftPanel(MenuPanelDB.IdentPanel.SelectRoom);
        }

        public override void OnJoinedRoom()
        {
            StartCoroutine(Utility.PhotonUtility.LoadYourAsyncScene("SelectCharacter"));
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            string roomName = "";
            for (int i = 0; i < 5; ++i)
            {
                var buf = (char)Random.Range('a', 'Z');
                roomName += buf;
            }

            Utility.PhotonUtility.CreateAndJoinRoom(roomName, true);
        }
    }
}