using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

public class RandomMatchButton : MatchingButton
{
    protected override void ToConnectRoom()
    {
        PhotonUtility.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Guid guid = new Guid();
        PhotonUtility.CreateAndJoinRoom(guid.ToString().Substring(0,4), true);
    }
}
