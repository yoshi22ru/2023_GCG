using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Sources.OutGame.MenuScene;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;


namespace Sources.OutGame.SelectCharacterScene.FirstEdition
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