﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BluePriest : MonoBehaviourPunCallbacks, IPunObservable
{
    public Rigidbody2D rb;
    public Animator an;
    public SpriteRenderer sr;
    public PhotonView pv;
    public Transform fireBall;

    public int curHealth;

    bool isMove;
    bool isDamage;
    bool isFireReady;

    float fireReady;
    

    Vector3 curPos;




    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (pv.IsMine)
        {
            fireReady += Time.deltaTime;
            if(fireReady > 2f)
            {
                isFireReady = true;
            }
            if (isMove == true)
            {
                an.SetBool("walk", true);
                Move();
            }
            else
                an.SetBool("walk", false);
            Debug.DrawRay(rb.position + (Vector2.up) + (Vector2.left * 0.7f), Vector2.left * 1.3f, new Color(1, 0, 0));
            RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up + (Vector2.left * 0.7f), Vector2.left, 1.3f);
            if (hit.collider == null || hit.collider.tag == "Blue")
                isMove = true;
            else if (hit.collider.tag == "Red" && isFireReady == true)
            {
                isMove = false;
                pv.RPC("AttackRPC", RpcTarget.AllBuffered);
                PhotonNetwork.Instantiate("BFireBall", fireBall.position, Quaternion.Euler(0, 0, -90));
                fireReady = 0;
                isFireReady = false;
                //StartCoroutine(Attack());
            }
            else
            {
                isMove = false;
            }
        }
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }


    void Move()
    {
        transform.position = new Vector2(transform.position.x + (-1f * Time.deltaTime), transform.position.y);
    }

    [PunRPC]
    void AttackRPC()
    {
        an.SetTrigger("attack");
    }

    [PunRPC]
    void DestoryRPC()
    {
        an.SetTrigger("die");
        Destroy(gameObject, 0.2f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "RedMelee")
        {
            if (!isDamage)
            {
                Weapon weapon = other.GetComponent<Weapon>();
                curHealth -= weapon.damage;
                StartCoroutine("OnDamage");

                if (curHealth <= 0)
                {
                    StopAllCoroutines();
                    pv.RPC("DestoryRPC", RpcTarget.AllBuffered);
                }
            }

        }
    }

    IEnumerator OnDamage()
    {
        isDamage = true;
        yield return new WaitForSeconds(1.3f);

        isDamage = false;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
        }
    }
}
