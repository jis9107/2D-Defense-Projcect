using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameControll : MonoBehaviour
{
    public void ClickKnight()
    {
        PhotonNetwork.Instantiate("Knight", Vector3.zero, Quaternion.identity);
    }
}
