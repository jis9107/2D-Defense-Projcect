using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameControll : MonoBehaviourPunCallbacks
{
    public GameObject panel;
    public GameObject gamestartPanel;

    public Text knightPrice;
    public Text soldierPrice;

    float _money;

    public void ClickKnight()
    {
        int _knightPrice = int.Parse(knightPrice.text);
        _money = FindObjectOfType<NetworkManager>().userMoney;
        if(_money >= _knightPrice)
        {
            PhotonNetwork.Instantiate("Knight", Vector3.zero, Quaternion.identity);
            _money -= _knightPrice; 
        }
    }

    private void Update()
    {
        
    }

}
