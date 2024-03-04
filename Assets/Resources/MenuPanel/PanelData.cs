using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(PanelData), menuName = "ScriptableObjects/CreatePanelData")]
public class PanelData : ScriptableObject
{
    [SerializeField]
    private MenuPanelDB.IdentPanel panel_name;
    [SerializeField]
    private GameObject panel_prefab;

    public MenuPanelDB.IdentPanel PanelName {get => panel_name;}
    public GameObject PanelPrefab {get => panel_prefab;}
}
