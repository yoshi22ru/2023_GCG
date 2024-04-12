using UnityEngine;
using UnityEngine.UI;

namespace Utility.MatchingScene
{
    public class PlayerIconView: MonoBehaviour
    {
        public int OwnerNumber => ownerNumber;
        public Text PlayerNumber => playerNumber;
        public Image CharacterIcon => characterIcon;
        public Text PlayerName => playerName;
        [SerializeField] private int ownerNumber;
        [SerializeField] private Text playerName;
        [SerializeField] private Text playerNumber;
        [SerializeField] private Image characterIcon;
    }
}