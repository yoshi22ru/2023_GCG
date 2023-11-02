using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PanelManager : MonoBehaviourPunCallbacks
{
    public static PanelManager instance;
    Ident_Panel now_panel = Ident_Panel.EndEnum;
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

    public override void OnDisconnected(DisconnectCause cause)
    {
      Debug.Log("Disconnected");
      Debug.Log(cause);
    }


    public enum Ident_Panel {
        // SelectBattleMode,
        SelectMatchingMode,
        // ChangeName,
        InputRoomName,
        EndEnum,
    }
}
