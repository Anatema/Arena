using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LobbyController : MonoBehaviour
{

    [SerializeField]
    private PanelsMenu _panelsMenu;
    [SerializeField]
    private PhotonController _controller;


    private List<RoomInfo> _avaliableRoomsCache = new List<RoomInfo>();
    public UnityEvent<List<RoomInfo>> OnRoomListUpdated = new UnityEvent<List<RoomInfo>>();


    private void Start()
    {
        ConnectToPhoton();
        _controller.OnJoinedLobbyEvent.AddListener(OpenNamePanel);
        _controller.OnRoomListUpdatedEvent.AddListener(RoomListUpdated);
    }
    private void ConnectToPhoton()
    {
        _panelsMenu.OpenPanel(0);
        _controller.ConnectToPhoton();
    }   
    public void OpenNamePanel()
    {
        _panelsMenu.OpenPanel(1);
    }

    public List<RoomInfo> GetRoomsInfo()
    {
       return _avaliableRoomsCache;
    }
   
    public void RoomListUpdated(List<RoomInfo> roomList)
    {
        _avaliableRoomsCache = roomList;
        OnRoomListUpdated?.Invoke(roomList);
    }
    public void SetName(string name)
    {
        _controller.SetName(name);
    }
    public void OpenJoinPanel()
    {
        _panelsMenu.OpenPanel(2);
    }
    public void JoinRoom(string roomName)
    {
        _controller.JoinRoom(roomName);
    }
    public void CreateRoom(string roomName)
    {
        _controller.CreateAndJoinRomm(roomName);
    }

    private void OnDestroy()
    {
        _controller.OnJoinedLobbyEvent.RemoveAllListeners();
        _controller.OnRoomListUpdatedEvent.RemoveAllListeners();
    }
}
