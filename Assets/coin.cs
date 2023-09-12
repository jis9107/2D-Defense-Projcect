using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class coin : MonoBehaviourPunCallbacks
{
    public PhotonView pv;
    GameControll _game;
    // Start is called before the first frame update
    void Start()
    {
        if (!pv.IsMine)
        {
            _game = FindObjectOfType<GameControll>();
            _game.userMoney += 2;
            Destroy(this.gameObject);
        }
        if (pv.IsMine)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
