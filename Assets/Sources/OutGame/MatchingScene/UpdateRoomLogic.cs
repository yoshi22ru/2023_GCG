using System;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using Resources.Character;
using UnityEngine;

namespace Sources.OutGame.MatchingScene
{
    public class UpdateRoomLogic
    {
        private readonly PlayerViewEntity _entity;
        private readonly CharaDataBase _charaDataBase;
        
        public UpdateRoomLogic(PlayerViewEntity entity, CharaDataBase charaDataBase)
        {
            _entity = entity;
            _charaDataBase = charaDataBase;
        }
        
        public void Update()
        {
            var playerList = PhotonNetwork.PlayerList;
            
            foreach (var player in playerList)
            {
                UpdateOne(player);
            }
        }

        public void UpdateOne(Player player)
        {
            var views = _entity.Views;
            var actorNumber = player.ActorNumber - 1;
            views[actorNumber].PlayersCharacterName.text = player.NickName;

            if (player.TryGetCharacter(out var character))
            {
                views[actorNumber].CharacterIcon.sprite = _charaDataBase.GetSprite(character);
                views[actorNumber].PlayersCharacterName.text = Enum.GetName(typeof(CharaData.IdentCharacter), character);
                views[actorNumber].PlayerName.text = player.NickName;
            }
            else
            {
                Debug.Log("Custom Property not Set");
            }
        }
    }
}