using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// 첫 입장 클라이언트는 방을 만들며 입장하고,
// 일정 조건(1분, 10명)동안 클라이언트들의 입장(Join)을 받는다.
public class JoinManager : MonoBehaviourPunCallbacks
{
    // ConnectScene Objects
    [SerializeField] GameObject connectScene;

    // 방 이름
    string roomName = "GameRoom";

    // 플레이어 정원
    [SerializeField] int maxPlayerNum = 10;
    [SerializeField] ConnectTimeCount counting;

    public TMP_Text headCountText;

    // 플레이어 오브젝트
    GameObject player;

    // Photon view 플레이어
    [SerializeField] PhotonView playerPrefab;
    // 리스폰 위치
    [SerializeField] Transform[] playerSpawnPoints;
    // 맵 오브젝트
    [SerializeField] GameObject map;

    // 현재 접속한 클라이언트의 ActorNumber(고유 넘버) (아마 입장 순일듯)
    int actorNumber;

    private void Start()
    {
        CreatRoom();
    }

    private void Update()
    {
        // 현재 접속자 수 UI 텍스트로 출력
        if (headCountText != null && PhotonNetwork.CurrentRoom != null)
        {
            headCountText.text = (PhotonNetwork.CurrentRoom.PlayerCount).ToString() + " / " + maxPlayerNum;
        }

        // 정원이 다 들어왔거나 제한 시간이 끝났다면,
        if (PhotonNetwork.CurrentRoom != null && counting != null)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayerNum ||
                counting.IsTimeOver)
            {
                // 게임 시작
                LoadScene();
            }
        }
    }

    // 로비에 방을 만든다.
    public void CreatRoom()
    {
        // 방을 roomName 이름으로 만든다.(이미 만들어져 있다면 Join)
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayerNum }, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        player = PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPoints[actorNumber].position, Quaternion.identity);
        player.SetActive(false);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("방 개설 실패.");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("방 입장 실패.");
    }

    void LoadScene()
    {
        // 게임이 시작되므로 입장 제한
        PhotonNetwork.CurrentRoom.IsOpen = false;

        connectScene.SetActive(false);
        map.SetActive(true);
        player.SetActive(true);
        Camera.main.GetComponent<CameraRoatate>().enabled = true;

        //SceneManager.LoadScene("GameScene");
    }
}