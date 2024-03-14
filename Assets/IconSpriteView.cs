using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharaData;
using UnityEngine.UI;

public class IconSpriteView : MonoBehaviour
{
    [SerializeField] CharaData charaData;
    private Sprite charaSprite;
    public Image charaImage;

    void Start()
    {
        var data = charaData.CharaSprite;
        charaSprite = data;
        charaImage.sprite = charaSprite;
    }
}
