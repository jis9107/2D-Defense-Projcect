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
    public GameObject loadingPanel;
    public GameObject gameStartPanel;
    public GameControll gameControl;

    public Text currentText;
    //public bool inGame;
    //public float userMoney;

    //public Text nickNameText;
    //public Text moneyText;
    //public Image healthImage;

    private void Awake()
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    //public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public void JoinRandomOrCreateRoom()
    {
        string nick = NickNameInput.text;

        PhotonNetwork.LocalPlayer.NickName = nick;

        byte maxPlayers = 2;
        int maxTime = 300;

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "maxTime", maxTime } }; // 게임 시간 지정.
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "maxTime" }; // 여기에 키 값을 등록해야, 필터링이 가능하다.

        PhotonNetwork.JoinRandomOrCreateRoom(
        expectedCustomRoomProperties: new ExitGames.Client.Photon.Hashtable() { { "maxTime", maxTime } }, expectedMaxPlayers: maxPlayers, // 참가할 때의 기준.
        roomOptions: roomOptions // 생성할 때의 기준.
        );
    }

    public void CancelMatching()
    {

    }

    private void UpdatePlayerCounts()
    {
        currentText.text = $"{PhotonNetwork.CurrentRoom.PlayerCount} / {PhotonNetwork.CurrentRoom.MaxPlayers}";
    }

    public override void OnConnectedToMaster()
    {
        print("서버 접속 완료");
        //PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        //PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 2 }, null);
    }

    public override void OnJoinedRoom()
    {
        print("방 참가 완료");

        Debug.Log($"{PhotonNetwork.LocalPlayer.NickName}은 인원수 {PhotonNetwork.CurrentRoom.MaxPlayers} 매칭 기다리는 중 ");
        UpdatePlayerCounts();

        DisconnectPanel.SetActive(false);
        loadingPanel.SetActive(true);

        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            loadingPanel.SetActive(false);
            gameStartPanel.SetActive(true);
        }

        //gameStartPanel.SetActive(true);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"플레이어 {newPlayer.NickName} 방 참가");
        UpdatePlayerCounts();

        if (PhotonNetwork.IsMasterClient)
        {
            gameControl = FindObjectOfType<GameControll>();
            gameControl._state = GameControll.State.Red;
        }
        //if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        //{
        //    loadingPanel.SetActive(false);
        //    gameStartPanel.SetActive(true);
        //}
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected)
    //    {
    //        PhotonNetwork.Disconnect();
    //    }
    //}

    public override void OnDisconnected(DisconnectCause cause)
    {
        DisconnectPanel.SetActive(true);
        gameStartPanel.SetActive(false);
    }
    public void Spawn()
    {
        RespawnPanel.SetActive(false);
    }
}

