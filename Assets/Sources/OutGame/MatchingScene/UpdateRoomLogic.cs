using Photon.Pun;
using Resources.Character;

namespace Utility.MatchingScene
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
            foreach (var view in _entity.Views)
            foreach (var player in playerList)
            {
                view.PlayerName.text = player.NickName;
            }
        }
    }
}