﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class sword : MonoBehaviourPunCallbacks, IPunObservable
{
    public PhotonView pv;
    public BoxCollider2D meleeArea;

    StatusDataBase _status;

    public int damage;

    string _name;


    void Awake()
    {
        
    }
    void Start()
    {
        if (pv.IsMine)
        {
            _status = FindObjectOfType<StatusDataBase>();
            damage = _status.knightDamage;
        }
    }

    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D col) // col을 RPC의 매개변수로 넘겨줄 수 없다
    {
        if (!pv.IsMine && col.tag == "Red" && col.GetComponent<PhotonView>().IsMine) // 느린쪽에 맞춰서 Hit판정
        {
            _name = col.gameObject.name;
            switch(_name){
                
                case "RedKnight(Clone)" :
                    col.GetComponent<RedKnightScript>().Hit(damage);
                    pv.RPC("EndSwingRPC", RpcTarget.AllBuffered);
                    break;

                case "RedPriest(Clone)":
                    col.GetComponent<RedPriest>().Hit(damage);
                    pv.RPC("EndSwingRPC", RpcTarget.AllBuffered);
                    break;

                case "RedMerchant(Clone)":
                    col.GetComponent<RedMerchant>().Hit(damage);
                    pv.RPC("EndSwingRPC", RpcTarget.AllBuffered);
                    break;

                case "RedBase(Clone)":
                    col.GetComponent<RedCamp>().Hit(damage);
                    pv.RPC("EndSwingRPC", RpcTarget.AllBuffered);
                    break;

                default :
                    break;
            }
        }

        if (!pv.IsMine && col.tag == "Blue" && col.GetComponent<PhotonView>().IsMine) // 느린쪽에 맞춰서 Hit판정
        {
            _name = col.gameObject.name;
            switch (_name)
            {

                case "BlueKnight(Clone)":
                    col.GetComponent<BlueKnightScript>().Hit(damage);
                    pv.RPC("EndSwingRPC", RpcTarget.AllBuffered);
                    break;

                case "BluePriest(Clone)":
                    col.GetComponent<BluePriest>().Hit(damage);
                    pv.RPC("EndSwingRPC", RpcTarget.AllBuffered);
                    break;

                case "BlueMerchant(Clone)":
                    col.GetComponent<BlueMerchant>().Hit(damage);
                    pv.RPC("EndSwingRPC", RpcTarget.AllBuffered);
                    break;

                case "BlueBase(Clone)":
                    col.GetComponent<BlueCamp>().Hit(damage);
                    pv.RPC("EndSwingRPC", RpcTarget.AllBuffered);
                    break;


                default:
                    break;
            }
        }
    }

    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);
    
    [PunRPC]
    void EndSwingRPC()
    {
        meleeArea.enabled = false;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(damage);
        }
        else
        {
            damage = (int)stream.ReceiveNext();
        }
    }
}
