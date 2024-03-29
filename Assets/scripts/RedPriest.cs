﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RedPriest : MonoBehaviourPunCallbacks, IPunObservable
{
    public Rigidbody2D rb;
    public Animator an;
    public SpriteRenderer sr;
    public PhotonView pv;
    public Transform fireBall;

    StatusDataBase _status;

    public int curHealth;
    public int damage;

    bool isMove;
    bool isFireReady;

    float _moveSpeed;
    float fireReady;


    Vector3 curPos;




    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();


    }

    void Start()
    {
        if (pv.IsMine)
        {
            _status = FindObjectOfType<StatusDataBase>();
            curHealth = _status.priestHealth;
            damage = _status.priestDamage;
            _moveSpeed = _status.moveSpeed;
        }
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (pv.IsMine)
        {
            fireReady += Time.deltaTime;
            if (fireReady > 2f)
            {
                isFireReady = true;
            }
            if (isMove == true)
            {
                an.SetBool("walk", true);
                Move();
            }
            else if(isMove == false)
                an.SetBool("walk", false);
            Debug.DrawRay(rb.position + (Vector2.up) + (Vector2.right * 0.7f), Vector2.right * 1.5f, new Color(1, 0, 0));
            RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up + (Vector2.right * 0.7f), Vector2.right, 1.4f, 1 << LayerMask.NameToLayer("Blue"));

            if (hit.collider == null)
            {
                isMove = true;
            }
            else
            {
                isMove = false;
                if (isFireReady == true)
                {
                    pv.RPC("AttackRPC", RpcTarget.AllBuffered);
                    StartCoroutine(Attack());
                    isFireReady = false;
                    fireReady = 0;
                }

            }

        }
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.85f);
        PhotonNetwork.Instantiate("RFireBall", rb.position + (Vector2.up) + (Vector2.right * 0.7f), Quaternion.Euler(0, 0, 90));
    }


    void Move()
    {
        transform.position = new Vector2(transform.position.x + (_moveSpeed * Time.deltaTime), transform.position.y);
    }

    [PunRPC]
    void AttackRPC()
    {
        an.SetTrigger("attack");
    }

    [PunRPC]
    void DestoryRPC()
    {
        if (!pv.IsMine)
        {
            GameControll _game = FindObjectOfType<GameControll>();
            _game.userMoney += 5;
        }
        an.SetTrigger("die");
        Destroy(gameObject, 0.2f);
    }

    public void Hit(int damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
        {
            StopAllCoroutines();
            pv.RPC("DestoryRPC", RpcTarget.AllBuffered);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(curHealth);
            stream.SendNext(damage);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            curHealth = (int)stream.ReceiveNext();
            damage = (int)stream.ReceiveNext();
        }
    }
}
