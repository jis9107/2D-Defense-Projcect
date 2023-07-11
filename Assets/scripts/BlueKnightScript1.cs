﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BlueKnightScript : MonoBehaviourPunCallbacks, IPunObservable
{
    GameControll.State _state;

    public Rigidbody2D rb;
    public Animator an;
    public SpriteRenderer sr;
    public PhotonView pv;

    bool isMove;
    bool isAttack;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }

    void Awake()
    {
        an = GetComponent<Animator>();
    }

    void Start()
    {
        //_state = FindObjectOfType<GameControll>()._state;
    }

    void Update()
    {
        Move();
        //if (_state == GameControll.State.Red)
        //{
        //    RedMove();
        //}
        //if(_state == GameControll.State.Blue)
        //{
        //    BlueMove();
        //}
    }

    //void RedMove()
    //{
    //    transform.position = new Vector2(transform.position.x + 3f * Time.deltaTime, transform.position.y);
    //}
    
    //void BlueMove() 
    //{
    //    transform.position = new Vector2(-transform.position.x + 3f * Time.deltaTime, transform.position.y);
    //}
    void Move()
    {
            transform.position = new Vector2(transform.position.x - 3f * Time.deltaTime, transform.position.y);    
    }

}