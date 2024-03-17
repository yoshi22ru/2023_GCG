using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharaData;
using UnityEngine.UI;
using Resources.Character;

public class IconSpriteView : MonoBehaviour
{
    [SerializeField] CharaDataBase charaData;
    private Sprite[] charaSprite = new Sprite[9];
    public Image[] charaImage = new Image[9];

    void Start()
    {
        for (int i = 0; i < charaImage.Length; i++)
        {
            var data = charaData.characterData[i].CharaSprite;
            charaSprite[i] = data;
            charaImage[i].sprite = charaSprite[i];
        }
    }
}
