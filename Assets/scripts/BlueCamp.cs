﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class BlueCamp : MonoBehaviourPunCallbacks, IPunObservable
{
    public PhotonView pv;

    public Image healthImage;

    public SpriteRenderer sr;

    public Text nickName;

    public BoxCollider2D box;

    GameControll _gamecontrol;

    bool isDamage;

    // Start is called before the first frame update
    void Start()
    {
        _gamecontrol = FindObjectOfType<GameControll>();
        if (_gamecontrol._state == GameControll.State.Blue)
            nickName.text = PhotonNetwork.NickName;
        if (_gamecontrol._state == GameControll.State.Red)
            nickName.text = pv.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
            if (other.tag == "RedMelee")
            {
                if (isDamage == false)
                {
                    Weapon weapon = other.GetComponent<Weapon>();
                    healthImage.fillAmount -= weapon.damage / 100f;
                    StartCoroutine("OnDamage");

                    if (healthImage.fillAmount <= 0)
                    {
                        pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
                        pv.RPC("RedTeamWin", RpcTarget.AllBuffered);
                        //if (_gamecontrol._state == GameControll.State.Blue)
                        //{
                        //    GameObject.Find("Canvas").transform.Find("LosePanel").gameObject.SetActive(true);
                        //}
                        //if (_gamecontrol._state == GameControll.State.Red)
                        //{
                        //    GameObject.Find("Canvas").transform.Find("WinPanel").gameObject.SetActive(true);
                        //}
                        //_gamecontrol.gamestartPanel.SetActive(false);
                    }
                }

            }
        

    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(healthImage.fillAmount);
        }
        else
        {
            healthImage.fillAmount = (float)stream.ReceiveNext();
        }
    }
    IEnumerator OnDamage()
    {
        isDamage = true;
        yield return new WaitForSeconds(1f);
        isDamage = false;
    }

    public void Hit()
    {
        healthImage.fillAmount -= 0.1f;
        if (healthImage.fillAmount <= 0)
        {
            pv.RPC("DestroyRPC", RpcTarget.AllBuffered); // AllBuffered로 해야 제대로 사라져 복제버그가 안 생긴다
        }
    }

    [PunRPC]
    void RedTeamWin()
    {
        _gamecontrol = FindObjectOfType<GameControll>();
        _gamecontrol.RedWin();
    }


}
