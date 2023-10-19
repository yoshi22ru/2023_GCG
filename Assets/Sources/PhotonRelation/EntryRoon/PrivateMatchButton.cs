using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class PrivateMatchButton : MatchingButton
{
    [SerializeField] private GameObject input_roomname_window;
    protected override void ToConnectRoom()
    {
        input_roomname_window.SetActive(true);
    }

}
