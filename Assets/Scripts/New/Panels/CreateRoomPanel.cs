using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomPanel : Panel
{
    [SerializeField]
    private LobbyController _lobbyScene;
    [SerializeField]
    private TMP_InputField _createRoomNameInputField;
    [SerializeField]
    private Button _createButton;

    private void Start()
    {
        _createRoomNameInputField.onValueChanged.AddListener(OnTextChanged);
    }
    private void OnTextChanged(string value)
    {
        _createButton.interactable = value.Length > 0;
    }
    public override void OpenPanel()
    {
        base.OpenPanel();
        _createRoomNameInputField.text = string.Empty;
        _createButton.interactable = false;
        _createButton.onClick.AddListener(() => CreateRoom());
    }
    public void CreateRoom()
    {
        _lobbyScene.CreateRoom(_createRoomNameInputField.text);
    }
    private void OnDestroy()
    {
        _createButton.onClick.RemoveAllListeners();
        _createRoomNameInputField.onValueChanged.RemoveAllListeners();
    }

}
