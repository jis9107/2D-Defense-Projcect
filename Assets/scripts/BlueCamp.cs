using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class BlueCamp : MonoBehaviourPunCallbacks, IPunObservable
{
    public PhotonView pv;

    public Image healthImage;

    public BoxCollider2D box;

    GameControll _gamecontrol;

    bool isDamage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "RedMelee")
        {
            if (!isDamage)
            {
                Weapon weapon = other.GetComponent<Weapon>();
                healthImage.fillAmount -= weapon.damage / 100f;
                StartCoroutine("OnDamage");

                if(healthImage.fillAmount <= 0)
                {
                    _gamecontrol = FindObjectOfType<GameControll>();
                    if(_gamecontrol._state == GameControll.State.Blue)
                    {
                        GameObject.Find("Canvas").transform.Find("LosePanel").gameObject.SetActive(true);
                    }
                    if (_gamecontrol._state == GameControll.State.Red)
                    {
                        GameObject.Find("Canvas").transform.Find("WinPanel").gameObject.SetActive(true);
                    }
                    _gamecontrol.gamestartPanel.SetActive(false);
                }
            }

        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
    IEnumerator OnDamage()
    {
        isDamage = true;
        yield return new WaitForSeconds(1f);
        isDamage = false;
    }

}
