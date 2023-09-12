using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class sword : MonoBehaviourPunCallbacks
{
    public PhotonView pv;
    public BoxCollider2D meleeArea;

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
                
                case "RedKnight(Clone)" :
                    col.GetComponent<RedKnightScript>().Hit(10);
                    pv.RPC("EndSwingRPC", RpcTarget.AllBuffered);
                    break;

                case "RedPriest(Clone)":
                    col.GetComponent<RedPriest>().Hit(10);
                    pv.RPC("EndSwingRPC", RpcTarget.AllBuffered); break;

                case "RedBase(Clone)":
                    col.GetComponent<RedCamp>().Hit(10);
                    pv.RPC("EndSwingRPC", RpcTarget.AllBuffered); break;

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
                    col.GetComponent<BlueKnightScript>().Hit(10);
                    pv.RPC("EndSwingRPC", RpcTarget.AllBuffered);
                    break;

                case "BluePriest(Clone)":
                    col.GetComponent<BluePriest>().Hit(10);
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
}
