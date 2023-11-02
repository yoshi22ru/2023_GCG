using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class StartBattleButton : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button start_button;

    private void Start() {
        start_button.onClick.AddListener(StartBattle);
    }

    void StartBattle() {
        if (photonView.IsRoomView) {
            switch (Random.Range(1,2)) {
            case 1:
                SceneManager.LoadScene("Stage1");
                break;
            case 2:
                SceneManager.LoadScene("Stage2");
                break;
            }
        }
    }
}
