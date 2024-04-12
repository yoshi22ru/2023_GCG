using System;
using Photon.Pun;
using Resources.Character;
using UnityEngine;

namespace Utility.MatchingScene
{
    public class MatchingContainer: MonoBehaviourPunCallbacks
    {
        [SerializeField] private PlayerIconView[] view;
        [SerializeField] private CharaDataBase dataBase;

        private UpdateRoomLogic _updateRoomLogic;

        private void Awake()
        {
            var playerViewEntity = new PlayerViewEntity();
            
            _updateRoomLogic = new UpdateRoomLogic(playerViewEntity, dataBase);
        }
        
        public override void OnJoinedRoom()
        {
            _updateRoomLogic.Update();
            var buf = PhotonNetwork.PlayerList;
        }
    }
}