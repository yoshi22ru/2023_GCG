using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.OutGame.SelectCharacterScene.SecondEdition
{
    public class ShowRoomName: MonoBehaviourPunCallbacks
    {
        [SerializeField] private Text text;

        private void Start()
        {
            Debug.Log(PhotonNetwork.CurrentRoom.Name);
            text.text = PhotonNetwork.CurrentRoom.Name;
        }

        public override void OnJoinedRoom()
        {
            text.text = PhotonNetwork.CurrentRoom.Name;
        }
    }
}