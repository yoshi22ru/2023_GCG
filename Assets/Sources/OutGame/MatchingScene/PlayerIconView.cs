using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sources.OutGame.MatchingScene
{
    public class PlayerIconView: MonoBehaviour
    {
        public int OwnerNumber => ownerNumber;
        public Text PlayerName => playerName;
        public Image CharacterIcon => characterIcon;
        public Text PlayersCharacterName => playersCharacterName;
        
        [SerializeField] private int ownerNumber;
        [SerializeField] private Text playersCharacterName;
        [FormerlySerializedAs("playerNumber")] [SerializeField] private Text playerName;
        [SerializeField] private Image characterIcon;

        public void SetOwnerNumber(int number)
        {
            ownerNumber = number;
        }
    }
}