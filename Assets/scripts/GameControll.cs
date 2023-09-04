using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameControll : MonoBehaviourPunCallbacks
{
    public enum State
    {
        Red,
        Blue
    }

    public State _state;

    public GameObject panel;
    public GameObject gamestartPanel;
    public GameObject redWinPanel;
    public GameObject blueWinPanel;

    public Text knightPrice;
    public Text soldierPrice;
    public Text thiefPrice;
    public Text moneyText;

    public Transform redSpawn;
    public Transform blueSpawn;

    public int userMoney;
    public float redCountMoney;
    public float blueCountMoney;
    public int _money;
    bool inGame;


    private void Awake()
    {
        inGame = false;
        userMoney = 20;
    }

    private void Start()
    {

    }
    public void ClickKnight()
    {
        int _knightPrice = int.Parse(knightPrice.text);
        if(userMoney >= _knightPrice)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate("RedKnight", redSpawn.position, Quaternion.Euler(0, -180, 0));
                userMoney -= _knightPrice;
            }
            if(!PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate("BlueKnight", blueSpawn.position, Quaternion.Euler(0, 0, 0));
                userMoney -= _knightPrice;
            }
        }
    }
    public void ClickSoldier()
    {
        int _soldierPrice = int.Parse(soldierPrice.text);
        if (userMoney >= _soldierPrice)
        {
            if (_state == State.Red)
            {
                PhotonNetwork.Instantiate("RedSoldier", redSpawn.position, Quaternion.Euler(0, -180, 0));
                userMoney -= _soldierPrice;
            }
            if (_state == State.Blue)
            {
                PhotonNetwork.Instantiate("BlueSoldier", blueSpawn.position, Quaternion.Euler(0, 0, 0));
                userMoney -= _soldierPrice;
            }
        }
    }
    public void ClickThief()
    {
        int _thiefPrice = int.Parse(thiefPrice.text);
        if (userMoney >= _thiefPrice)
        {
            if (_state == State.Red)
            {
                PhotonNetwork.Instantiate("RedThief", redSpawn.position, Quaternion.Euler(0, -180, 0));
                userMoney -= _thiefPrice;
            }
            if (_state == State.Blue)
            {
                PhotonNetwork.Instantiate("BlueThief", blueSpawn.position, Quaternion.Euler(0, 0, 0));
                userMoney -= _thiefPrice;
            }
        }
    }

    private void Update()
    {
        if (gamestartPanel.activeSelf == true)
        {
            inGame = true;
            if(inGame == true)
            {
                //userMoney = Time.deltaTime;
                //_money = (int)userMoney;
                //moneyText.text = _money.ToString();
              
                
                moneyText.text = userMoney.ToString();
            }
        }
    }

    [PunRPC]
    public void RedWin()
    {
        gamestartPanel.SetActive(false);
        redWinPanel.SetActive(true);
    }

    [PunRPC]
    public void BlueWin()
    {
        gamestartPanel.SetActive(false);
        blueWinPanel.SetActive(true);
    }

    //public void KillRed(int money)
    //{
    //    switch (money)
    //    {
    //        case 5:
    //            userMoney += 5f;
    //            break;

    //        case 10:
    //            userMoney += 10f;
    //            break;

    //        case 15:
    //            userMoney += 15f;
    //            break;

    //    }
    //}
    //public void KillBlue(int money)
    //{
    //    switch (money)
    //    {
    //        case 5:
    //            userMoney += 5f;
    //            break;

    //        case 10:
    //            userMoney += 10f;
    //            break;

    //        case 15:
    //            userMoney += 15f;
    //            break;

    //    }
    //}

}
