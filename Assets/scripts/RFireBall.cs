using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RFireBall : MonoBehaviourPunCallbacks
{

    public PhotonView pv;

    public int damage;

    string _name;

    // Start is called before the first frame update

    private void Awake()
    {
        StatusDataBase _status = FindObjectOfType<StatusDataBase>();
        damage = _status.priestDamage;
    }
    void Start()
    {
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
                    col.GetComponent<BlueKnightScript>().Hit(damage);
                    pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
                    break;

                case "BluePriest(Clone)":
                    col.GetComponent<BluePriest>().Hit(damage);
                    pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
                    break;

                case "BlueMerchant(Clone)":
                    col.GetComponent<BluePriest>().Hit(damage);
                    pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
                    break;

                case "BlueBase(Clone)":
                    col.GetComponent<BlueCamp>().Hit(damage);
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
