using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Sources.BattleObject;
using Unity.VisualScripting;

public class StartBattleButton : MonoBehaviourPunCallbacks
{
    [SerializeField] private List<SelectCharacter> selections;
    [SerializeField] private Button start_button;

    private void Awake()
    {
        for (int i = 0; i < selections.Count; ++i)
        {
            selections[i].SetActorNum(i + 1);
            selections[i].ChangeTeam();
        }
    }
    private void Start()
    {
        start_button.onClick.AddListener(StartBattle);
    }

    void FixedUpdate()
    {
        CheckAll();

        if (Input.GetKey(KeyCode.Return))
        {
            Debug.Log("return key");
            for (int i = 0; i < selections.Count; ++i)
            {
                selections[i].OnDecision();
            }
        }
        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Backspace))
        {
            for (int i = 0; i < selections.Count; ++i)
            {
                selections[i].OffDecision();
            }
        }
    }

    void CheckAll()
    {
        int i = 0;
        for (; i < selections.Count; ++i)
        {
            if (!selections[i].GetDecision())
            {
                break;
            }
        }

        if (i == selections.Count & IsEven())
        {
            SelectPanelManager.instance
                .SetPanel(SelectPanelManager.Ident_Panel.StartPanel);
        }
        else
        {
            SelectPanelManager.instance
                .SetPanel(SelectPanelManager.Ident_Panel.DefaultPanel);
        }

        if (PhotonNetwork.OfflineMode && selections[0].GetDecision())
        {
            SelectPanelManager.instance
                .SetPanel(SelectPanelManager.Ident_Panel.StartPanel);
        }
    }

    void StartBattle()
    {
        // if (photonView.IsRoomView)
        // {
        //     Debug.Log("you are not owner");
        //     return;
        // }


        photonView.RPC(nameof(RPCLoadScene), RpcTarget.All);
    }

    [PunRPC]
    void RPCLoadScene()
    {
        SetParams();
        switch (Random.Range(1, 2))
        {
            case 1:
                SceneManager.LoadScene("Stage1");
                break;
            case 2:
                SceneManager.LoadScene("Stage2");
                break;
        }
    }

    bool IsEven()
    {
        int count = 0;

        for (int i = 0; i < selections.Count; ++i)
        {
            if (selections[i].GetTeam() == BattleObject.Team.Blue)
            {
                ++count;
            }
        }

        if (count == selections.Count / 2)
        {
            return true;
        }
        return false;
    }

    void SetParams()
    {
        for (int i = 0; i < selections.Count; ++i)
        {
            VariableManager.player_selections.Add(new VariableManager.PlayerSelection(
                    selections[i].GetTeam(),
                    selections[i].GetCharacter(),
                    selections[i].GetActorNum()
            ));
        }
    }
}
