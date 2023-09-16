using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RedMerchant : MonoBehaviourPunCallbacks, IPunObservable
{
    public Rigidbody2D rb;
    public Animator an;
    public SpriteRenderer sr;
    public PhotonView pv;

    GameControll _game;

    public int curHealth;

    float fireReady;
    float _moveSpeed;

    bool isMove;

    Vector3 curPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        _game = FindObjectOfType<GameControll>();
        StatusDataBase _status = FindObjectOfType<StatusDataBase>();
        curHealth = _status.knightHealth;
        _moveSpeed = _status.moveSpeed;
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
            if (fireReady > 1.5f)
            {
                GetMoney();
            }
            if (isMove == true)
            {
                an.SetBool("walk", true);
                Move();
            }
            else
                an.SetBool("walk", false);
            Debug.DrawRay(rb.position + (Vector2.up) + (Vector2.right * 0.7f), Vector2.right * 0.1f, new Color(1, 0, 0));
            RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up + (Vector2.right * 0.7f), Vector2.right, 0.1f);
            if (hit.collider == null)
                isMove = true;
            else if (hit.collider != null)
            {
                isMove = false;
            }
        }
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }


    void Move()
    {
        transform.position = new Vector2(transform.position.x + _moveSpeed * Time.deltaTime, transform.position.y);

    }


    [PunRPC]
    void DestoryRPC()
    {
        if (!pv.IsMine)
        {
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

    public void GetMoney()
    {
        _game.userMoney += 1;
        fireReady = 0;
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
