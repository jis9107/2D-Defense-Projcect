using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public InputField NickNameInput;

    public GameObject DisconnectPanel;
    public GameObject RespawnPanel;
    public bool inGame;
    public float money;

    //public Text nickNameText;
    public Text moneyText;
    //public Image healthImage;

    private void Awake()
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 2 }, null);
    }

    public override void OnJoinedRoom()
    {
        DisconnectPanel.SetActive(false);
        inGame = true;
        Spawn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }

        if (inGame == true)
        {
            money += Time.deltaTime;
            int _money = (int)money;
            moneyText.text = _money.ToString();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        DisconnectPanel.SetActive(true);
        RespawnPanel.SetActive(false);
    }
    public void Spawn()
    {
        //PhotonNetwork.Instantiate("Knight", Vector3.zero, Quaternion.identity);
        RespawnPanel.SetActive(false);
    }
}

