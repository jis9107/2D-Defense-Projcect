using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class StatusDataBase : MonoBehaviourPunCallbacks
{
    public int knightDamage;
    public int priestDamage;

    public int knightHealth;
    public int priestHealth;
    public int merchantHealth;

    public float moveSpeed;

    //현재 공격력 및 체력
    public Text _knightDamage;
    public Text _priestDamage;

    public Text _knightHealth;
    public Text _priestHealth;
    public Text _merchantHealth;


    public void UpdateStatus()
    {
        _knightDamage.text = knightDamage.ToString();
        _priestDamage.text = priestDamage.ToString();

        _knightHealth.text = knightHealth.ToString();
        _priestHealth.text = priestHealth.ToString();
        _merchantHealth.text = merchantHealth.ToString();
    }
}
