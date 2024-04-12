using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CharaData;
using UnityEngine.UI;
using Resources.Character;
using Sources.SelectCharacterScene.SecondEdition;
using UnityEngine.SceneManagement;
using Utility.SelectCharacterScene.SecondEdition;

public class IconSpriteManager : MonoBehaviour, ICharaSelectCallable
{
    [SerializeField] private Button goToBattleScene;
    [SerializeField] private SelectedCharaData selectedCharaData;
    [SerializeField] private Image selectedImage;
    [SerializeField] private CharaDataBase charaData;
    private IconView[] _iconViews;
    
    void Start()
    {
        goToBattleScene.onClick.AddListener(GoToBattle);
        var spriteViews = FindObjectsOfType<IconSpriteView>();
        _iconViews = new IconView[spriteViews.Length];
        for (int i = 0; i < spriteViews.Length; i++)
        {
            var sprite = charaData.GetSprite(i);
            if (sprite is null)
            {
                return;
            }
            spriteViews[i].Image.sprite = sprite;
            spriteViews[i].Manager = this;
            _iconViews[i] = new IconView(spriteViews[i], charaData.characterData[i].CharaName);
        }
    }

    private void GoToBattle()
    {
        if (!selectedCharaData.IsSelect) return;
        // FIXME
        switch (0)
        {
            case 0:
                // Matching
                // Load Scene
                break;
        }
        
    }

    public void CharaSelectCall(IconSpriteView view)
    {
        var selected = _iconViews.FirstOrDefault(x => x.View == view);
        selectedCharaData.SetCharacter(charaData.GetCharaData(selected.IdentCharacter));
    }

    struct IconView
    {
        public IconView(IconSpriteView view, IdentCharacter identCharacter)
        {
            View = view;
            IdentCharacter = identCharacter;
        }
        public readonly IconSpriteView View;
        public readonly IdentCharacter IdentCharacter;
    }
}
