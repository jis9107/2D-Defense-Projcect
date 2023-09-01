using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Weapon : MonoBehaviourPunCallbacks, IPunObservable
{
    public PhotonView pv;

    public enum Type
    {
        Knight,
        Soldier,
        Thief
    }
    public Type type;

    public int damage;
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        if (type == Type.Knight)
            damage = 10;
        if (type == Type.Soldier)
            damage = 12;
        if (type == Type.Thief)
            damage = 15;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (pv.IsMine)
        {
            if(other.tag == "BlueCamp")
            {
                float enemyhealth = other.gameObject.GetComponent<BlueCamp>().healthImage.fillAmount;
                if(enemyhealth <= 0)
                {
                    GameControll _gamecontrol = FindObjectOfType<GameControll>();
                    _gamecontrol.RedWin();
                }
            }

            if(other.tag == "RedCamp")
            {
                float enemyhealth = other.gameObject.GetComponent<RedCamp>().healthImage.fillAmount;
                if (enemyhealth <= 0)
                {
                    GameControll _gamecontrol = FindObjectOfType<GameControll>();
                    _gamecontrol.BlueWin();
                }
            }
        }
    }
}
