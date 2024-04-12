using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.SelectCharacterScene.SecondEdition
{
    public class SelectedCharaData: MonoBehaviour
    {
        public static CharaData.IdentCharacter SelectedCharacter
        {
            get;
            private set;
        }

        public bool IsSelect
        {
            get;
            private set;
        }
        [SerializeField] private Image image;
        [SerializeField] private Text text;

        public void SetCharacter(CharaData character)
        {
            image.sprite = character.CharaSprite;
            text.text = Enum.GetName(typeof(CharaData.IdentCharacter), character.CharaName);
            SelectedCharacter = character.CharaName;
            IsSelect = true;
        }
    }
}