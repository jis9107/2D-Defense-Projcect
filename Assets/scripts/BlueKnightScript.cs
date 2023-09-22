using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BlueKnightScript : MonoBehaviourPunCallbacks, IPunObservable
{
    public Rigidbody2D rb;
    public Animator an;
    public SpriteRenderer sr;
    public PhotonView pv;
    public BoxCollider2D meleeArea;
    public Transform sword;

    StatusDataBase _status;

    int curHealth;

    float _moveSpeed;
    float fireReady;

    bool isMove;
    bool isDamage;
    bool isFireReady;

    Vector3 curPos;




    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        _status = FindObjectOfType<StatusDataBase>();

    }

    void Start()
    {
        isFireReady = false;
        curHealth = _status.knightHealth;
        _moveSpeed = _status.moveSpeed;
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
            Debug.DrawRay(rb.position + (Vector2.up) + (Vector2.left * 0.7f), Vector2.left * 0.1f, new Color(1, 0, 0));
            RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up + (Vector2.left * 0.7f), Vector2.left, 0.1f);
            if (hit.collider == null)
                isMove = true;
            else if(hit.collider != null)
            {
                isMove = false;
                if (hit.collider.tag == "Red" && isFireReady == true)
                {
                    isMove = false;
                    isFireReady = false;
                    pv.RPC("AttackRPC", RpcTarget.AllBuffered);
                    StartCoroutine(Attack());
                    fireReady = 0;
                }
            }

        }
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }


    void Move()
    {
        transform.position = new Vector2(transform.position.x + (-_moveSpeed * Time.deltaTime), transform.position.y);
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
            _game.userMoney += 3;
        }
        an.SetTrigger("die");
        Destroy(gameObject, 0.2f);
    }

    [PunRPC]
    void SwingRPC()
    {
        meleeArea.enabled = true;
    }


    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
        pv.RPC("SwingRPC", RpcTarget.AllBuffered);
    }

    public void Hit(int damage)
    {
        curHealth -= damage;
        if(curHealth <= 0)
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
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
        }
    }
}
