using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Weapon : MonoBehaviourPunCallbacks, IPunObservable
{
    public PhotonView pv;

    public enum Type
    {
        Knight,
        Priest,
        Thief
    }
    public Type type;

    public int damage;
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        if (type == Type.Knight)
            damage = 10;
        if (type == Type.Priest)
            damage = 15;
        if (type == Type.Thief)
            damage = 15;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }
}
