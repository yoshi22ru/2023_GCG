using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
  public class PhotonUtility : MonoBehaviourPunCallbacks
  {
    [Header("DefaultSettings")]
    #region DefaultSettings

    [SerializeField] static int maxPlayer = 4;
    [SerializeField] static bool isOpen = true;
    #endregion

    #region LocalVariables

    #endregion

    #region RappedFunction

    public static IEnumerator LoadYourAsyncScene(string scene_name) {
      AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene_name);

      while (!asyncLoad.isDone) {
        yield return null;
      }
      Debug.Log("Loaded");
    }
    public static void Connect(string GameVersion)
    {
      if (PhotonNetwork.IsConnected == false)
      {
        PhotonNetwork.GameVersion = GameVersion;
        PhotonNetwork.ConnectUsingSettings();
      }
      else
      {
        Debug.Log("Already connected");
      }
    }

    public static void SetMyName(string nicName)
    {
      if (PhotonNetwork.IsConnected)
      {
        PhotonNetwork.LocalPlayer.NickName = nicName;
        Debug.Log("NicName : " + nicName);
      }
      else
      {
        Debug.Log("Not Connected");
      }
    }

    private static void JoinLoby()
    {
      if (!PhotonNetwork.IsConnected) return;
      if (PhotonNetwork.JoinLobby())
      {
        Debug.Log("Join Loby");
      }
      else
      {
        Debug.Log("Join Loby Failed");
      }
    }

    public static void CreateAndJoinRoom(string roomName, bool isVisible)
    {
      RoomOptions roomOptions = new RoomOptions
      {
        MaxPlayers = (byte)maxPlayer,
        IsVisible = isVisible,
        IsOpen = isOpen,
        CleanupCacheOnLeave = true
      };

      // if (PhotonNetwork.InLobby)
      // {
        PhotonNetwork.CreateRoom(roomName, roomOptions);
      // }
    }

    public static void JoinSelectRoom(string targetRoomName)
    {
      if (PhotonNetwork.InLobby)
      {
        PhotonNetwork.JoinRoom(targetRoomName);
      }
    }

    public static void JoinRandomRoom()
    {
      if (PhotonNetwork.InLobby)
      {
        PhotonNetwork.JoinRandomRoom();
      }
    }

    public static void LeaveRoom()
    {
      if (PhotonNetwork.InRoom)
      {
        PhotonNetwork.LeaveRoom();
      }
      else
      {
        Debug.Log("already leaved");
      }
    }
    #endregion

    #region OriginalCallBackFunction

    /// <summary>
    /// Initialize Custom Paramater
    /// </summary>
    public virtual void InitializeRoom() { }

    /// <summary>
    /// Write process after Joined Room
    /// </summary>
    public virtual void AfterJoinedRoom() { }

    #endregion

    #region CallBackFunction

    public override void OnConnected()
    {
      Debug.Log("OnConnected");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
      Debug.Log("Disconnected");
      Debug.Log(cause);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
      Debug.Log("OnRoomListUpdate");

    }

    public override void OnConnectedToMaster()
    {
      Debug.Log("OnConnectedToMaster");

      JoinLoby();
    }

    public override void OnJoinedLobby()
    {
      Debug.Log("OnJoinedLobby");
    }

    public override void OnLeftLobby()
    {
      Debug.Log("OnLeftLobby");
    }

    public override void OnCreatedRoom()
    {
      Debug.Log("OnCreatedRoom");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
      Debug.Log("OnCreateRoomFailed");
      PhotonNetwork.Disconnect();
    }

    public override void OnJoinedRoom()
    {
      Debug.Log("OnJoinedRoom");

      if (PhotonNetwork.InRoom)
      {
        Debug.Log("RoomName: " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("HostName: " + PhotonNetwork.MasterClient.NickName);
        Debug.Log("Slots: " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers);
      }

      if (PhotonNetwork.IsMasterClient)
      {
        InitializeRoom();
      }
      AfterJoinedRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
      Debug.Log("OnJoinRoomFailed");
      Debug.Log(returnCode + " : " + message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
      Debug.Log("OnJoinRandomRoomFailed");
      Debug.Log(returnCode + " : " + message);
    }

    public override void OnLeftRoom()
    {
      Debug.Log("OnLeftRoom");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
      Debug.Log("OnPlayerEnterdRoom");
      Debug.Log(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
      Debug.Log("OnPlayerLeftRoom");
      Debug.Log(otherPlayer);
    }



    #endregion

  }
}
