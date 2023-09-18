﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BFireBall : MonoBehaviourPunCallbacks
{

    public PhotonView pv;

    public int _damage;

    string _name;

    // Start is called before the first frame update
    private void Awake()
    {
        BluePriest _status = FindObjectOfType<BluePriest>();
        _damage = _status.damage;
    }
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + (-3f * Time.deltaTime), transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!pv.IsMine && col.tag == "Red" && col.GetComponent<PhotonView>().IsMine) // 느린쪽에 맞춰서 Hit판정
        {
            _name = col.gameObject.name;
            switch (_name)
            {

                case "RedKnight(Clone)":
                    col.GetComponent<RedKnightScript>().Hit(_damage);
                    pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
                    break;

                case "RedPriest(Clone)":
                    col.GetComponent<RedPriest>().Hit(_damage);
                    pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
                    break;

                case "RedMerchant(Clone)":
                    col.GetComponent<RedMerchant>().Hit(_damage);
                    pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
                    break;

                case "RedBase(Clone)":
                    col.GetComponent<RedCamp>().Hit(_damage);
                    pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
                    break;

                default:
                    break;
            }
        }
    }

    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);

}
