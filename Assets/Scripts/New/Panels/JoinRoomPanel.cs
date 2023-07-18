using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;

public class JoinRoomPanel : Panel
{
    [SerializeField]
    private Transform _contaioner;
    [SerializeField]
    private LobbyController _lobbyScene;
    [SerializeField]
    private Button _joinButton;
    [SerializeField]
    private Toggle _togglePrefab;
    [SerializeField]
    private ToggleGroup _toggleGroup;
    private string _selectedRoom;
    public void Start()
    {
        _lobbyScene.OnRoomListUpdated.AddListener(OnRoomListUpdated);
        _joinButton.onClick.AddListener(JoinRoom);
        //on value changed
    }
    private void OnRoomListUpdated(List<RoomInfo> roomInfo)
    {
        Clear();
        RefeshData(roomInfo);
    }
    private void ToggleChanged(Toggle toggle, bool value)
    {
        if (value)
        {
            _selectedRoom = toggle.GetComponentInChildren<TextMeshProUGUI>().text;
        }
    }
    private void JoinRoom()
    {
        _lobbyScene.JoinRoom(_selectedRoom);
    }
    private void RefeshData(List<RoomInfo> roomInfo)
    {
        foreach (RoomInfo room in roomInfo)
        {
            Toggle toggle = Instantiate(_togglePrefab, _contaioner);
            toggle.GetComponentInChildren<TextMeshProUGUI>().text = room.Name;
            toggle.group = _toggleGroup;
            toggle.onValueChanged.AddListener((bool b) => ToggleChanged(toggle, b));
        }

        if(roomInfo.Count > 0)
        {
            _selectedRoom = _toggleGroup.GetFirstActiveToggle().GetComponentInChildren<TextMeshProUGUI>().text;            
        }
        _joinButton.interactable = _lobbyScene.GetRoomsInfo().Count != 0;
        
    }
    public override void OpenPanel()
    {
        base.OpenPanel();
        Clear();
        RefeshData(_lobbyScene.GetRoomsInfo());
    }
    private void Clear()
    {
        foreach (Transform child in _contaioner)
        {
            Destroy(child.gameObject);
        }
    }
    private void OnDestroy()
    {
        _lobbyScene.OnRoomListUpdated.RemoveListener(OnRoomListUpdated);
        _joinButton.onClick.RemoveAllListeners();
    }
}
