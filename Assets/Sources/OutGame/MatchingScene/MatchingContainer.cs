using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Resources.Character;
using Sources.InGame.BattleObject;
using UnityEngine;

namespace Sources.OutGame.MatchingScene
{
    public class MatchingContainer: MonoBehaviourPunCallbacks
    {
        [SerializeField] private PlayerIconView[] views;
        [SerializeField] private CharaDataBase dataBase;

        private UpdateRoomLogic _updateRoomLogic;

        private async void Awake()
        {
            var playerViewEntity = new PlayerViewEntity(views[0], views[1], views[2], views[3]);

            _updateRoomLogic = new UpdateRoomLogic(playerViewEntity, dataBase);
            _updateRoomLogic.Update();

            await UniTask.WaitUntil(() => PhotonNetwork.PlayerList.Length == 4);


            // 素敵なSomething
            foreach (var player in PhotonNetwork.PlayerList)
            {
                player.TryGetCharacter(out var character);
                VariableManager.PlayerSelections.Add(new VariableManager.PlayerSelection(
                    player.ActorNumber % 2 == 0 ? Team.Red : Team.Blue,
                    character,
                    player.ActorNumber
                ));
            }

            btnFX.SceneToBattleScene();
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