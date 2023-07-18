using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsCreator : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Vector2 _minBorders;
    [SerializeField]
    private Vector2 _maxBorders;
    [SerializeField]
    private int _numberOfcoins;
    [SerializeField]
    private GameObject _coin;
    public void CreateCoins()
    {
        for(int i = 0; i < _numberOfcoins; i++)
        {
            float xPos = Random.Range(_minBorders.x, _maxBorders.x);
            float zPos = Random.Range(_minBorders.y, _maxBorders.y);
            PhotonNetwork.Instantiate(_coin.name, new Vector3(xPos, 1, zPos), Quaternion.Euler(90, 0, 0));
        }
    }
}
