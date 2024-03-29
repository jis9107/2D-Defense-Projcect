﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public InputField NickNameInput;

    public GameObject DisconnectPanel;
    public GameObject loadingPanel;
    public GameObject gameStartPanel;

    public GameControll gameControl;
    public StatusDataBase status;

    public Text currentText;
    public Text redNick;
    public Text blueNick;

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

    private void UpdatePlayerCounts()
    {
        currentText.text = $"{PhotonNetwork.CurrentRoom.PlayerCount} / {PhotonNetwork.CurrentRoom.MaxPlayers}";

        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            currentText.text = "매칭 완료, 3초 후 시작합니다.";
            Invoke("GameStart", 3f);
        }
    }

    public override void OnConnectedToMaster()
    {
        print("서버 접속 완료");
    }

    public override void OnJoinedRoom()
    {
        print("방 참가 완료");

        DisconnectPanel.SetActive(false);
        loadingPanel.SetActive(true);
        Debug.Log($"{PhotonNetwork.LocalPlayer.NickName}은 인원수 {PhotonNetwork.CurrentRoom.MaxPlayers} 매칭 기다리는 중 ");

        if (!PhotonNetwork.IsMasterClient)
        {
            gameControl = FindObjectOfType<GameControll>();
            gameControl._state = GameControll.State.Blue;
            //blueNick.text = PhotonNetwork.LocalPlayer.NickName;
            //redNick.text = PhotonNetwork.MasterClient.NickName;
            PhotonNetwork.Instantiate("BlueBase", new Vector3(8.19f, -0.709f, 0), Quaternion.Euler(0, 0, 0));
        }

        UpdatePlayerCounts();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"플레이어 {newPlayer.NickName} 방 참가");

        if (PhotonNetwork.IsMasterClient)
        {
            gameControl = FindObjectOfType<GameControll>();
            gameControl._state = GameControll.State.Red;
            //redNick.text = PhotonNetwork.LocalPlayer.NickName;
            //blueNick.text = newPlayer.NickName;
            PhotonNetwork.Instantiate("RedBase", new Vector3(-8.3f, -0.72f, 0), Quaternion.Euler(0, 0, 0));
        }

        UpdatePlayerCounts();

    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected)
        {
            ExitGame();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        DisconnectPanel.SetActive(true);
        gameStartPanel.SetActive(false);
    }

    public void GameStart()
    {
        status.InitializationStatus();
        gameControl.InitializationGame();
        loadingPanel.SetActive(false);
        gameStartPanel.SetActive(true);
    }
    public void ExitGame()
    {
        PhotonNetwork.Disconnect();
    }
}

