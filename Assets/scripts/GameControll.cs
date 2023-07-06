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

    public Text knightPrice;
    public Text soldierPrice;
    public Text thiefPrice;
    public Text moneyText;

    public Transform redSpawn;
    public Transform blueSpawn;

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
            if(_state == State.Red)
            {
                PhotonNetwork.Instantiate("Knight", redSpawn.position, Quaternion.Euler(0, -180, 0));
                userMoney -= _knightPrice;
            }
            if(_state == State.Blue)
            {
                PhotonNetwork.Instantiate("Knight", blueSpawn.position, Quaternion.identity);
                userMoney -= _knightPrice;
            }
 
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
        int _thiefPrice = int.Parse(thiefPrice.text);
        if (userMoney >= _thiefPrice)
        {
            PhotonNetwork.Instantiate("Thief", Vector3.zero, Quaternion.identity);
            userMoney -= _thiefPrice;
        }
    }

    private void Update()
    {
        if (gamestartPanel.activeSelf == true)
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
