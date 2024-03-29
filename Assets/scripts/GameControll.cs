﻿using System.Collections;
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
    public GameObject gamestartPanel;
    public GameObject redWinPanel;
    public GameObject blueWinPanel;
    public GameObject startPanel;
    public GameObject connectPanel;
    public GameObject explainPanel;
    public GameObject explainUpPanel;
    public GameObject[] panels;

    //아군 생성
    public Text knightPrice;
    public Text soldierPrice;
    public Text merchantPrice;
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

    public Image[] spawnImages;

    public bool spawnReady;

    public int userMoney;
    bool inGame;


    private void Awake()
    {

        inGame = false;
        spawnReady = true;
        userMoney = 20;
    }

    private void Start()
    {

    }
    public void ClickKnight()
    {
        int _knightPrice = int.Parse(knightPrice.text);
        if(userMoney >= _knightPrice && spawnReady == true)
        {
            SpawnTime();
            if (PhotonNetwork.IsMasterClient)
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
    public void ClickPriest()
    {
        int _priestPrice = int.Parse(soldierPrice.text);
        if (userMoney >= _priestPrice && spawnReady == true)
        {
            SpawnTime();
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
    public void ClickMerchant()
    {
        int _merchantPrice = int.Parse(merchantPrice.text);
        if (userMoney >= _merchantPrice && spawnReady == true)
        {
            SpawnTime();
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate("RedMerchant", redSpawn.position, Quaternion.Euler(0, -180, 0));
                userMoney -= _merchantPrice;
            }
            if (!PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate("BlueMerchant", blueSpawn.position, Quaternion.Euler(0, 0, 0));
                userMoney -= _merchantPrice;
            }
        }
    }

    private void Update()
    {
        if (gamestartPanel.activeSelf == true)
        {
            inGame = true;
            if (inGame == true)
            {
                moneyText.text = userMoney.ToString();
            }
            if (_state == State.Red && PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                gamestartPanel.SetActive(false);
                redWinPanel.SetActive(true);
            }
            if (_state == State.Blue && PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                gamestartPanel.SetActive(false);
                blueWinPanel.SetActive(true);
            }
        }
        else
        {
            inGame = false;
        }




    }

    public void UpgradeDamage()
    {
            int _attackUpPrice = int.Parse(attackUpPrice.text);
            if (userMoney >= _attackUpPrice)
            {
                status.knightDamage += 5;
                status.priestDamage += 8;
                userMoney -= _attackUpPrice;
                _attackUpPrice += 2;
                attackUpPrice.text = _attackUpPrice.ToString();
                if (_attackUpPrice > 10)
                {
                    attackUpButton.SetActive(false);
                    attackMax.SetActive(true);
                }
                status.UpdateStatus();
            }
        

    }


    public void UpgradeHealth()
    {
            int _healthUpPrice = int.Parse(healthUpPrice.text);
            if (userMoney >= _healthUpPrice)
            {
                status.knightHealth += 10;
                status.priestHealth += 5;
                status.merchantHealth += 5;
                userMoney -= _healthUpPrice;
                _healthUpPrice += 2;
                healthUpPrice.text = _healthUpPrice.ToString();
                if (_healthUpPrice > 10)
                {
                    healthUpButton.SetActive(false);
                    healthMax.SetActive(true);
                }

                status.UpdateStatus();

            }
        

    }

    public void UpgradeMoveSpeed()
    {
            int _movespUpPrice = int.Parse(movespUpPrice.text);
            if (userMoney >= _movespUpPrice)
            {
                status.moveSpeed += 0.1f;
                userMoney -= _movespUpPrice;
                _movespUpPrice += 2;
                movespUpPrice.text = _movespUpPrice.ToString();
                if (_movespUpPrice > 10)
                {
                    movespUpButton.SetActive(false);
                    movespMax.SetActive(true);
                }
                status.UpdateStatus();

            }
        

    }

    public void SpawnTime()
    {
        spawnReady = false;
        for (int i = 0; i < spawnImages.Length; i++)
        {
            spawnImages[i].fillAmount = 0;
        }
    }

    public void InitializationGame()
    {
        userMoney = 20;
        attackUpPrice.text = "2";
        healthUpPrice.text = "2";
        movespUpPrice.text = "2";

    }




    public void MoveToConnectPanel()
    {
        PanelManager();
        connectPanel.SetActive(true);
    }

    public void MoveToExplainUpPanel()
    {
        PanelManager();
        explainUpPanel.SetActive(true);
    }

    public void MoveToExplainPanel()
    {
        PanelManager();
        explainPanel.SetActive(true);
    }

    public void MoveToStartPanel()
    {
        PanelManager();
        startPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PanelManager()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
    }
}
