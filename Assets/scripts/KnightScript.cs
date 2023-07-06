using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class KnightScript : MonoBehaviourPunCallbacks, IPunObservable
{
    GameControll.State state;

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
    void Start()
    {
        state = FindObjectOfType<GameControll>()._state;
    }

    void Update()
    {
        if(state == GameControll.State.Red)
        {
            RedMove();
        }
        if(state == GameControll.State.Blue)
        {
            BlueMove();
        }
    }

    void RedMove()
    {
        transform.position = new Vector2(transform.position.x + 3f * Time.deltaTime, transform.position.y);
    }
    
    void BlueMove() 
    {
        transform.position = new Vector2(-transform.position.x + 3f * Time.deltaTime, transform.position.y);
    }

}
