using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviourPunCallbacks
{
    private Transform _transform;
    [SerializeField]
    private float _rotationSeed;
    private void Awake()
    {
        _transform = transform;
    }
    private void Start()
    {
        transform.rotation = Quaternion.Euler(90, 0, Random.Range(0, 180));
    }
    private void OnTriggerEnter(Collider collision)
    {
        Player player = collision.transform.GetComponent<Player>();
        if (player && player.photonView.IsMine)
        {
            player.ReciveCoin();
            photonView.RPC("RemoveCoin", RpcTarget.AllBuffered);            
        }
    }
    [PunRPC]
    public void RemoveCoin()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        _transform.Rotate(new Vector3(0, 0, _rotationSeed * Time.deltaTime));
    }
}
