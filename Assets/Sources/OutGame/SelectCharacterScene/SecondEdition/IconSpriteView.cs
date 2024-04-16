using System;
using UnityEngine;
using UnityEngine.UI;

namespace Utility.SelectCharacterScene.SecondEdition
{
    public class IconSpriteView: MonoBehaviour
    {
        [HideInInspector]
        public ICharaSelectCallable Manager;
        
        public Button Button => button;
        public Image Image => image;
        [SerializeField] private Button button;
        [SerializeField] private Image image;

        private void Start()
        {
            button.onClick.AddListener(SelectCharacter);
        }

        private void SelectCharacter()
        {
            Manager?.CharaSelectCall(this);
        }
    }
}