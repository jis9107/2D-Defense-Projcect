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

    //게임 패널
    public GameObject panel;
    public GameObject gamestartPanel;
    public GameObject redWinPanel;
    public GameObject blueWinPanel;

    //아군 생성
    public Text knightPrice;
    public Text soldierPrice;
    public Text thiefPrice;
    public Text moneyText;

    public Transform redSpawn;
    public Transform blueSpawn;

    //업그레이드
    public Text attackUpPrice;
    public Text healthUpPrice;
    public Text movespUpPrice;

    public StatusDataBase status;

    public GameObject attackUpButton;
    public GameObject healthUpButton;
    public GameObject movespUpButton;

    public GameObject attackMax;
    public GameObject healthMax;
    public GameObject movespMax;


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

    public void UpgradeDamage()
    {
        int _attackUpPrice = int.Parse(attackUpPrice.text);
        if (userMoney >= _attackUpPrice)
        {
            status.knightDamage += 5;
            status.priestDamage += 8;
            _attackUpPrice += 2;
            attackUpPrice.text = _attackUpPrice.ToString();
            if(_attackUpPrice > 10)
            {
                attackUpButton.SetActive(false);
                attackMax.SetActive(true);
            }
        }
    }


    public void UpgradeHealth()
    {
        int _healthUpPrice = int.Parse(healthUpPrice.text);
        if (userMoney >= _healthUpPrice)
        {
            status.knightHealth += 10;
            status.priestHealth += 5;
            _healthUpPrice += 2;
            healthUpPrice.text = _healthUpPrice.ToString();
            if (_healthUpPrice > 10)
            {
                healthUpButton.SetActive(false);
                healthMax.SetActive(true);
            }

        }
    }


    public void UpgradeMoveSpeed()
    {
        int _movespUpPrice = int.Parse(movespUpPrice.text);
        if (userMoney >= _movespUpPrice)
        {
            status.moveSpeed += 0.1f;
            _movespUpPrice += 2;
            movespUpPrice.text = _movespUpPrice.ToString();
            if (_movespUpPrice > 10)
            {
                movespUpButton.SetActive(false);
                movespMax.SetActive(true);
            }

        }
    }

}
