using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class BlueCamp : MonoBehaviourPunCallbacks, IPunObservable
{
    public PhotonView pv;
    public Text nickNameText;
    public Image healthImage;

    // Start is called before the first frame update
    void Start()
    {
        if (pv.IsMine && !PhotonNetwork.IsMasterClient)
            nickNameText.text = PhotonNetwork.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }

}
