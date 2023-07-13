using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RedKnightScript : MonoBehaviourPunCallbacks, IPunObservable
{
    GameControll.State _state;

    public Rigidbody2D rb;
    public Animator an;
    public SpriteRenderer sr;
    public PhotonView pv;

    bool isAttack;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
    }

    void Start()
    {
    //    _state = FindObjectOfType<GameControll>()._state;
    }

    void Update()
    {
        //Vector2 attackRange = new Vector2(transform.position.x + 1, transform.position.y);
        //RaycastHit2D rayHit = Physics2D.Raycast(attackRange, Vector2.up,  LayerMask.GetMask("Red"));
        Move();
    }

    void Move()
    {
        transform.position = new Vector2(transform.position.x + 1f * Time.deltaTime, transform.position.y);
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other);
    //    if(other.tag == "Blue")
    //    {
    //        isAttack = true;
    //        an.SetTrigger("attack");
    //    }

    //}

}
