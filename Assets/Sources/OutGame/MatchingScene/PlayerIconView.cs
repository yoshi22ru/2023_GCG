using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sources.OutGame.MatchingScene
{
    public class PlayerIconView: MonoBehaviour
    {
        public int OwnerNumber => ownerNumber;
        public Text PlayerNumber => playerNumber;
        public Image CharacterIcon => characterIcon;
        public Text PlayersCharacterName => playersCharacterName;
        
        [SerializeField] private int ownerNumber;
        [SerializeField] private Text playersCharacterName;
        [SerializeField] private Text playerNumber;
        [SerializeField] private Image characterIcon;

        public void SetOwnerNumber(int number)
        {
            ownerNumber = number;
        }
    }
}