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
        yield return new WaitForSeconds(1.3f);
        isDamage = false;
    }

}
