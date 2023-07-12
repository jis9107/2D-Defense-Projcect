using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RedSoldier : MonoBehaviourPunCallbacks, IPunObservable
{
    public Rigidbody2D rb;
    public Animator an;
    public SpriteRenderer sr;
    public PhotonView pv;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

    void Awake()
    {

    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = new Vector2(transform.position.x + 1.5f * Time.deltaTime, transform.position.y);
    }

}
