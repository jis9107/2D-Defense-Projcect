using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RedKnightScript : MonoBehaviourPunCallbacks, IPunObservable
{
    public Rigidbody2D rb;
    public Animator an;
    public SpriteRenderer sr;
    public PhotonView pv;
    public BoxCollider2D meleeArea;

    public int curHealth;

    bool isMove;
    bool isDamage;


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
        
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
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
        else if (hit.collider.tag == "Blue")
        {
            isMove = false;
            pv.RPC("AttackRPC", RpcTarget.All);
            StartCoroutine(Attack());
        }
        else
        {
            isMove = false;
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
    void DestoryRPC()
    {
        an.SetTrigger("die");
        Destroy(gameObject, 0.2f);
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = true;
        yield return new WaitForSeconds(0.2f);
        meleeArea.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BlueMelee")
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



}
