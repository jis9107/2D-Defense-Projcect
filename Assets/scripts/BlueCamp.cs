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

    [PunRPC]
    void RedTeamWin()
    {
        _gamecontrol.gamestartPanel.SetActive(false);
        _gamecontrol.redWinPanel.SetActive(true);
        Destroy(this.gameObject);
    }

    public void Hit(int damage)
    {
        healthImage.fillAmount -= damage / 200f;
        if (healthImage.fillAmount <= 0)
        {
            pv.RPC("RedTeamWin", RpcTarget.AllBuffered);
        }
    }


}
