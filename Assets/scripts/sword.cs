using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class sword : MonoBehaviourPunCallbacks
{
    public PhotonView pv;

    string _name;


    void Start()
    {

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
                
                case "RedKnight" :
                    //col.GetComponent<RedKnightScript>().Hit(10);
                    pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
                    break;

                case "RedPriest":
                    //col.GetComponent<RedKnightScript>().Hit(10);
                    pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
                    break;

                default :
                    break;


            }

        }
    }

    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);
}
