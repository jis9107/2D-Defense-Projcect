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
    public GameObject meleeArea;

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
        if (isMove && !isAttack)
            Move();
        Debug.DrawRay(rb.position, Vector2.right, new Color(1, 0, 0));
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.right, 1, LayerMask.GetMask("Blue"));
        if(hit.collider != null)
        {
            isMove = false;
            isAttack = true;
            StartCoroutine(Attack());
        }
        else
        {
            isMove = true;
            isAttack = false;
        }

            
    }


    void Move()
    {
        transform.position = new Vector2(transform.position.x + 1f * Time.deltaTime, transform.position.y);

    }

    [PunRPC]
    void AttackRPC()
    {
        an.SetTrigger("attack");
    }

    [PunRPC]
    //void MoveRPC()
    //{
    //    an.SetBool("walk", true);
    //    Move();
    //}

    IEnumerator Attack()
    {
        pv.RPC("AttackRPC", RpcTarget.All);

        yield return new WaitForSeconds(0.2f);
        //meleeArea.enabled = true;

        //yield return new WaitForSeconds(1f);
        //meleeArea.enabled = false;
    }

    //void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.tag == "Blue")
    //    {
    //        StartCoroutine(Attack());
    //    }
    //}


}
