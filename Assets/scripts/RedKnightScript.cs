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

    bool isMove;
    bool attackReady;
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
        isMove = true;
    //    _state = FindObjectOfType<GameControll>()._state;
    }

    void Update()
    {
        if(isMove == true)
            Move();
        attackReady = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(1f, 0), 0.07f, 1 << LayerMask.NameToLayer("Blue"));
        if (attackReady && !isAttack)
            pv.RPC("AttackRPC", RpcTarget.All);
    }

    void Move()
    {
        transform.position = new Vector2(transform.position.x + 1f * Time.deltaTime, transform.position.y);
    }

    [PunRPC]
    void AttackRPC()
    {
        StartCoroutine(Attack());
    }

    [PunRPC]
    //void MoveRPC()
    //{
    //    an.SetBool("walk", true);
    //    Move();
    //}

    IEnumerator Attack()
    {
        isMove = false;
        isAttack = true;
        an.SetTrigger("attack");

        yield return new WaitForSeconds(0.2f);
        //meleeArea.enabled = true;

        //yield return new WaitForSeconds(1f);
        //meleeArea.enabled = false;

        isMove = true;
        isAttack = false;
    }


}
