﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Weapon : MonoBehaviour
{
    public enum Type
    {
        Knight,
        Soldier,
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
