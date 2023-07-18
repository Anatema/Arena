using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Shooter : MonoBehaviour
{
    public GameObject BulletPrefab;
    private float _cooldown = 0.4f;
    private float _nextTime = 0;

    
    public void Shoot()
    {
        if (Time.time > _nextTime)
        {
            _nextTime = Time.time + _cooldown;
            GameObject obj = PhotonNetwork.Instantiate(BulletPrefab.name, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            obj.transform.rotation = transform.rotation;
            obj.GetComponent<Bullet>().Initialize(GetComponent<Player>());
        }
    }
}
