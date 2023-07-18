using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class Player : MonoBehaviourPunCallbacks, IDamagable
{


    private Movement _movement;
    private Shooter _shooter;
    private InputUI _inputUI;
    [SerializeField]
    private int _health;
    public int Health => _health;
    [SerializeField]
    private int _maxHealth;
    public int MaxHealth => _maxHealth;

    public UnityEvent<Player> OnHealthChanged = new UnityEvent<Player>();
    public UnityEvent<Player> OnPlayerDestroyed = new UnityEvent<Player>();
    public void Awake()
    {
        _movement = GetComponent<Movement>();
        _shooter = GetComponent<Shooter>();
        _inputUI = InputUI.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateHealthBar();
        if (!photonView.IsMine)
        {
            Destroy(_movement);
            return;
        }
        _health = _maxHealth;
        
    }
    public void Update()
    {
        if (photonView.IsMine && _inputUI.Pressed)
        {
            _shooter.Shoot();
        }
    }
    private void CreateHealthBar()
    {
        HealthBarUIController.Instance.CreateHealthBar(this);
    }
    
    
    public void OnDestroy()
    {
        if (PhotonNetwork.InRoom)
        {
            Hashtable hashtable = photonView.Owner.CustomProperties;
            hashtable["Alive"] = false;
            photonView.Owner.SetCustomProperties(hashtable);
        }
        OnPlayerDestroyed?.Invoke(this);
        OnPlayerDestroyed.RemoveAllListeners();
        OnHealthChanged.RemoveAllListeners();
    }

    public void TakeDamage(int damage)
    {
        photonView.RPC("TakeDamageRPC", RpcTarget.AllBuffered, damage);
    }
    [PunRPC]
    public void TakeDamageRPC(int damage)
    {
        _health -= damage;
        OnHealthChanged?.Invoke(this);
        if (_health < 0 && photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    public void ReciveCoin()
    {
        photonView.RPC("ReciveCoinRPC", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void ReciveCoinRPC()
    {

        Hashtable hashtable = photonView.Owner.CustomProperties;
        int numberOfCoins = (int)hashtable["Coin"];
        hashtable["Coin"] = numberOfCoins + 1;
        photonView.Owner.SetCustomProperties(hashtable);
    }
}
