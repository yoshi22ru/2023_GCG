using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPanelManager : MonoBehaviour
{
    
    public static SelectPanelManager instance;
    Ident_Panel now_panel = Ident_Panel.DefaultPanel;
    [SerializeField] private List<GameObject> panels;
    private void Awake() {
        instance = this;
    }
    void Start()
    {
        SetPanel(now_panel);
    }

    public void SetPanel(Ident_Panel ident_Panel) {
        Debug.Log(ident_Panel);
        now_panel = ident_Panel;
        for (int i = 0; i < panels.Count; ++i) {
            if (i == (int) now_panel) {
                panels[i].SetActive(true);
            } else {
                panels[i].SetActive(false);
            }
        }
    }


    public enum Ident_Panel {
        DefaultPanel,
        StartPanel,
    }

}
