﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RFireBall : MonoBehaviourPunCallbacks, IPunObservable
{

    public PhotonView pv;

    RedPriest _status;

    public int _damage;

    string _name;

    // Start is called before the first frame update

    private void Awake()
    {
        _status = FindObjectOfType<RedPriest>();
        
    }
    void Start()
    {
        _damage = _status.damage;
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + (3f * Time.deltaTime), transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!pv.IsMine && col.tag == "Blue" && col.GetComponent<PhotonView>().IsMine) // 느린쪽에 맞춰서 Hit판정
        {
            _name = col.gameObject.name;
            switch (_name)
            {

                case "BlueKnight(Clone)":
                    col.GetComponent<BlueKnightScript>().Hit(_damage);
                    pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
                    break;

                case "BluePriest(Clone)":
                    col.GetComponent<BluePriest>().Hit(_damage);
                    pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
                    break;

                case "BlueMerchant(Clone)":
                    col.GetComponent<BlueMerchant>().Hit(_damage);
                    pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
                    break;

                case "BlueBase(Clone)":
                    col.GetComponent<BlueCamp>().Hit(_damage);
                    pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
                    break;


                default:
                    break;
            }
        }
    }

    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_damage);
        }
        else
        {
            _damage = (int)stream.ReceiveNext();
        }
    }
}
