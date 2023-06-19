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
    public Text moneyText;

    public float userMoney;
    int _money;
    bool inGame;

    private void Awake()
    {
        inGame = false;
    }
    public void ClickKnight()
    {
        int _knightPrice = int.Parse(knightPrice.text);
        if(userMoney >= _knightPrice)
        {
            PhotonNetwork.Instantiate("Knight", Vector3.zero, Quaternion.identity);
            userMoney -= _knightPrice; 
        }
    }
    public void ClickSoldier()
    {
        int _soldierPrice = int.Parse(soldierPrice.text);
        if (userMoney >= _soldierPrice)
        {
            PhotonNetwork.Instantiate("Soldier", Vector3.zero, Quaternion.identity);
            userMoney -= _soldierPrice;
        }
    }
    public void ClickThief()
    {
        int _knightPrice = int.Parse(knightPrice.text);
        if (userMoney >= _knightPrice)
        {
            PhotonNetwork.Instantiate("Knight", Vector3.zero, Quaternion.identity);
            userMoney -= _knightPrice;
        }
    }

    private void Update()
    {
        if (gamestartPanel)
        {
            inGame = true;
            if(inGame == true)
            {
                userMoney += Time.deltaTime;
                _money = (int)userMoney;
                moneyText.text = _money.ToString();
            }
        }
    }

}
