using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class StartBattleButton : MonoBehaviourPunCallbacks
{
    [SerializeField] private List<SelectCharacter> selections;
    [SerializeField] private Button start_button;

    private void Awake() {
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

    void StartBattle()
    {
        if (IsEven()) {
            Debug.Log("team is not even");
            return;
        }
        if (photonView.IsRoomView) {
            Debug.Log("you are not owner");
            return;
        }

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

    bool IsEven() {
        int count = 0;

        for (int i = 0; i < selections.Count; ++i) {
            if (selections[i].GetTeam() == BattleObject.Team.Blue) {
                ++count;
            }
        }

        if (count != selections.Count / 2) {
            return true;
        }
        return false;
    }
}
