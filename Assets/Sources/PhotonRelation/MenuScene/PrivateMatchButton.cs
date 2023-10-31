using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PrivateMatchButton : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject input_roomname_window;
    private void Start() {
        button.onClick.AddListener(ToConnectRoom);
    }
    public override void OnJoinedRoom()
    {
        SceneManager.LoadSceneAsync("CharacterSelect");
    }

    protected void ToConnectRoom() {
        input_roomname_window.SetActive(true);
    }
}
