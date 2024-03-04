using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = nameof(MenuPanelDB), menuName = "ScriptableObjects/CreatePanelDataBase")]
public class MenuPanelDB : ScriptableObject
{
    [FormerlySerializedAs("panelDatas")] [SerializeField] private List<PanelData> panelData = new List<PanelData>();

    public GameObject GetPanel(IdentPanel identPanel)
    {
        var panel = panelData
            .FirstOrDefault(data => data.PanelName == identPanel);
        if (panel is null)
        {
            throw new Exception("Panel not found");
        }
        else
        {
            return panel.PanelPrefab;
        }
    }
    
    
    public enum IdentPanel {
        SelectOnlineOrOffline,
        SelectRoomByRandomOrSelect,
        SelectRoom,
        SelectCharacter,
        StartGame,
    }
}
