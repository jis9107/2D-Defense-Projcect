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
        int _priestPrice = int.Parse(soldierPrice.text);
        if (userMoney >= _priestPrice)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate("RedPriest", redSpawn.position, Quaternion.Euler(0, -180, 0));
                userMoney -= _priestPrice;
            }
            if (!PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate("BluePriest", blueSpawn.position, Quaternion.Euler(0, 0, 0));
                userMoney -= _priestPrice;
            }
        }
    }
    public void ClickThief()
    {
        int _thiefPrice = int.Parse(thiefPrice.text);
        if (userMoney >= _thiefPrice)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate("RedThief", redSpawn.position, Quaternion.Euler(0, -180, 0));
                userMoney -= _thiefPrice;
            }
            if (!PhotonNetwork.IsMasterClient)
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

    public void UpgradeKnightDamage()
    {

    }

    public void UpgradePriestDamage()
    {

    }

    public void UpgradeKnightHealth()
    {

    }
    public void UpgradePriestHealth()
    {

    }

    public void UpgradeMoveSpeed()
    {

    }

}
