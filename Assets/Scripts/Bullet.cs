using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Bullet : MonoBehaviourPun
{
    public float Speed;
    public int Damage;
    private bool isDestroyed = false;

    public Collider Collider;
    [SerializeField]
    private Player _Owner;
    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine)
        {
            Destroy(Collider);
            return;
        }
        StartCoroutine(DelayedDestroy(10f));
        
    }
    public void Initialize(Player owner)
    {
        _Owner = owner;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamagable>() != null)
        {
            if (!isDestroyed && _Owner!= null && other.gameObject != _Owner.gameObject)
            {
                other.GetComponent<IDamagable>().TakeDamage(Damage);
                isDestroyed = true;
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
    private IEnumerator DelayedDestroy(float timer)
    {
        yield return new WaitForSeconds(timer);
        PhotonNetwork.Destroy(gameObject);
    }
}
