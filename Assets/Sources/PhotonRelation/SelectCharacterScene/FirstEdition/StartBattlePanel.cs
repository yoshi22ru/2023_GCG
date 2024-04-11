using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Sources.PhotonRelation.MenuScene;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;


namespace Sources.PhotonRelation.SelectCharacterScene
{
    public class StartBattlePanel : MenuPanelBase
    {
        [SerializeField] private Button startButton;

        private void Start()
        {
            startButton.onClick.AddListener(StartBattle);
        }
        
        
        private void StartBattle()
        {
            startButton.interactable = false;
            Manager.StartGame();
        }
        
    }
}