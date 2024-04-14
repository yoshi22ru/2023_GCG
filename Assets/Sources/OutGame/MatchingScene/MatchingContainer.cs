using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Resources.Character;
using UnityEngine;

namespace Sources.OutGame.MatchingScene
{
    public class MatchingContainer: MonoBehaviourPunCallbacks
    {
        [SerializeField] private PlayerIconView[] views;
        [SerializeField] private CharaDataBase dataBase;

        private UpdateRoomLogic _updateRoomLogic;

        private void Awake()
        {
            var playerViewEntity = new PlayerViewEntity(views[0], views[1], views[2], views[3]);
            
            _updateRoomLogic = new UpdateRoomLogic(playerViewEntity, dataBase);
            _updateRoomLogic.Update();
        }

        private void Update()
        {
            if (!PhotonNetwork.OfflineMode)
            {
                PhotonNetwork.LocalPlayer.SendPlayerProperties();
            }
        }

        public override void OnJoinedRoom()
        {
            _updateRoomLogic.Update();
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            _updateRoomLogic.UpdateOne(targetPlayer);
        }
    }
}