using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameControll : MonoBehaviourPunCallbacks
{
    public GameObject panel;
    public Text knightPrice;
    public Text soldierPrice;
    public bool inGame;
    public float userMoney;
    public Text moneyText;

    public void ClickKnight()
    {
        int _knightPrice = int.Parse(knightPrice.text);
        if(userMoney >= _knightPrice)
            PhotonNetwork.Instantiate("Knight", Vector3.zero, Quaternion.identity);
    }

    void Update()
    {
        if (panel == false)
        {
            inGame = true;
            if (inGame == true)
            {
                userMoney += Time.deltaTime;
                int _money = (int)userMoney;
                moneyText.text = _money.ToString();
            }
        }
        else
            inGame = false;
    }
}
