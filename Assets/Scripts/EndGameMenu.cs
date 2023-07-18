using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameMenu : Panel
{
    [SerializeField]
    private TextMeshProUGUI _text;
    public void Initialized(Photon.Realtime.Player[] players)
    {
        foreach(Photon.Realtime.Player player in players)
        {
            if ((bool)player.CustomProperties["Alive"])
            {
                _text.text = $"Player: {player.NickName} Score : {player.CustomProperties["Coin"]}";
            }
        }
    }
}
