using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NamePanel : Panel
{
    [SerializeField]
    private LobbyController _lobbyScene;
    [SerializeField]
    private Button _sendButton;
    [SerializeField]
    private TMP_InputField _nameInpudField;
    private void Start()
    {
        _sendButton.onClick.AddListener(() => _lobbyScene.SetName(_nameInpudField.text));
        _sendButton.onClick.AddListener(_lobbyScene.OpenJoinPanel);
        _nameInpudField.onValueChanged.AddListener(OnTextChanged);
    }
    public override void OpenPanel()
    {
        _nameInpudField.text = string.Empty;
        _sendButton.interactable = false;
        base.OpenPanel();

    }
    private void OnTextChanged(string value)
    {
        _sendButton.interactable = value.Length > 0;
    }
    private void OnDestroy()
    {
        _sendButton.onClick.RemoveAllListeners();
        _nameInpudField.onValueChanged.RemoveAllListeners();
    }
}
