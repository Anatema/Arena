using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class BattleManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject _playerPrefab;
    public LevelState LevelState => _levelState;
    [SerializeField]
    private LevelState _levelState;
    [SerializeField]
    private int numberOfAlivePlayers = 0;
    [SerializeField]
    private EndGameMenu _endGameMenu;
    [SerializeField]
    private CoinsCreator _coinsCreator;
    private void Awake()
    {
        SetupPhoton();
    }
    private void SetupPhoton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LoadLevel(0);
            return;
        }

        _levelState = (LevelState)PhotonNetwork.CurrentRoom.CustomProperties["RoomState"];
        Hashtable hashtable = new Hashtable();
        hashtable.Add("Alive", true);
        hashtable.Add("Coin", 0);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);

        switch (_levelState)         
        {
            case (LevelState.Waiting):
                Debug.Log("Waiting for new players, no coins or shooting");
                //you are the first player so do nothing
                if (PhotonNetwork.CurrentRoom.Players.Count > 1)
                {
                    Debug.Log("Tou are the second player. game Starting");
                    Hashtable properties = PhotonNetwork.CurrentRoom.CustomProperties;
                    properties["RoomState"] = LevelState.Playing;
                    PhotonNetwork.CurrentRoom.SetCustomProperties(properties);
                }
                else
                {
                    _coinsCreator.CreateCoins();
                }
                break;

            case (LevelState.Playing):
                Debug.Log("Game already started, can shoot and collect coins");
                StartGame();
                break;

            case (LevelState.End):
                Debug.Log("Game already ended, kick player out");
                break;
        
        }


    }
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if(_levelState != (LevelState)propertiesThatChanged["RoomState"])
        {
            _levelState = (LevelState)propertiesThatChanged["RoomState"];
            switch (_levelState)
            {
                case (LevelState.Waiting):
                    Debug.Log("System returned to level state. that shpould not happen");
                    break;

                case (LevelState.Playing):
                    Debug.Log("System changed to playeng. allow all functions");
                    StartGame();
                    break;

                case (LevelState.End):
                    _endGameMenu.OpenPanel();
                    _endGameMenu.Initialized(PhotonNetwork.PlayerList);
                    Debug.Log("System changed to end game");
                    break;

            }
            //change level state
        }
        Debug.Log("property changed");
    }
    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps)
    {
        numberOfAlivePlayers = 0;
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            if ((bool)player.CustomProperties["Alive"])
            {
                numberOfAlivePlayers++;
            }
        }

        if (numberOfAlivePlayers < 2 && _levelState == LevelState.Playing && targetPlayer == PhotonNetwork.LocalPlayer)
        {
            Hashtable properties = PhotonNetwork.CurrentRoom.CustomProperties;
            properties["RoomState"] = LevelState.End;
            PhotonNetwork.CurrentRoom.SetCustomProperties(properties);
        }
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);    
        PhotonNetwork.LoadLevel(0);
    }
    public void LeaveLevel()
    {
        PhotonNetwork.Disconnect();
    }
    private void StartGame()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
        GameObject obj = PhotonNetwork.Instantiate(_playerPrefab.name, randomPosition, Quaternion.identity);
        
    }
    
}
