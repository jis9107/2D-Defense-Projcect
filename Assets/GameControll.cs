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

    public void ClickKnight()
    {
        int _knightPrice = int.Parse(knightPrice.text);
        if(_money >= _knightPrice)
        {
            PhotonNetwork.Instantiate("Knight", Vector3.zero, Quaternion.identity);
            _money -= _knightPrice; 
        }
    }

    private void Update()
    {
        if (gamestartPanel)
        {
            userMoney += Time.deltaTime;
            _money = (int)userMoney;
            moneyText.text = _money.ToString();
        }
    }

}
