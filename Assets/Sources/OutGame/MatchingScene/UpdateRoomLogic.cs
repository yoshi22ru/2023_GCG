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
            Debug.Log(playerList.Length);
            
            foreach (var player in playerList)
            {
                UpdateOne(player);
            }
        }

        public void UpdateOne(Player player)
        {
            var views = _entity.Views;
            Debug.Log(player.CustomProperties);
            views[player.ActorNumber].PlayersCharacterName.text = player.NickName;

            if (player.TryGetCharacter(out var character))
            {
                var actorNumber = player.ActorNumber - 1;
                views[actorNumber].CharacterIcon.sprite = _charaDataBase.GetSprite(character);
                views[actorNumber].PlayersCharacterName.text = Enum.GetName(typeof(CharaData.IdentCharacter), character);
            }
            else
            {
                Debug.Log("Custom Property not Set");
            }
        }
    }
}