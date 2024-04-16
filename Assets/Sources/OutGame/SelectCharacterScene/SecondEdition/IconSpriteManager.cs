using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using static CharaData;
using UnityEngine.UI;
using Resources.Character;
using Sources.SelectCharacterScene.SecondEdition;
using Sources.Sync;
using UnityEngine.SceneManagement;
using Utility.SelectCharacterScene.SecondEdition;

public class IconSpriteManager : MonoBehaviourPunCallbacks, ICharaSelectCallable
{
    [SerializeField] private Button goToBattleScene;
    [SerializeField] private SelectedCharaData selectedCharaData;
    [SerializeField] private CharaDataBase charaData;
    private IconView[] _iconViews;
    
    void Start()
    {
        goToBattleScene.onClick.AddListener(GoToMatching);
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

    private async void GoToMatching()
    {
        if (!selectedCharaData.IsSelect) return;

        PhotonNetwork.LocalPlayer.SetCharacter(selectedCharaData.SelectedCharacter);

        PhotonNetwork.GameVersion = "v1.0";
        PhotonNetwork.ConnectUsingSettings();

        // うまくいかない
        // await UniTask.WaitUntil(() => PhotonNetwork.IsConnectedAndReady);
        // PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(nameof(OnConnectedToMaster));
        var option = new RoomOptions();
        option.MaxPlayers = 4;
        option.IsOpen = VariableManager.RoomOption == RoomOption.Public;
        
        PhotonNetwork.JoinRandomOrCreateRoom(roomOptions:option);
    }

    public override void OnJoinedRoom()
    {
        btnFX.SceneToMatchingWaitFree();
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
