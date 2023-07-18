using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PhotonController : MonoBehaviourPunCallbacks
{
    public UnityEvent OnJoinedLobbyEvent = new UnityEvent();
    public UnityEvent<List<RoomInfo>> OnRoomListUpdatedEvent = new UnityEvent<List<RoomInfo>>();

    public void ConnectToPhoton()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
    public void CreateAndJoinRomm(string roomName)
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        Hashtable customProperties = new Hashtable();
        customProperties.Add("AlivePlayers", 0);
        customProperties.Add("RoomState", LevelState.Waiting);
        options.CustomRoomProperties = customProperties;
        PhotonNetwork.CreateRoom(roomName, options);
    }
    public void SetName(string name)
    {
        PhotonNetwork.NickName = name;
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        OnJoinedLobbyEvent.Invoke();
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        OnRoomListUpdatedEvent.Invoke(roomList);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
    }
}
