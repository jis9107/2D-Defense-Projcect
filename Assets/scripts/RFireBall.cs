﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RFireBall : MonoBehaviourPunCallbacks
{

    public PhotonView pv;
   
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * 4 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!pv.IsMine && col.tag == "Blue") // 느린쪽에 맞춰서 Hit판정
        {
            
            pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);
}
