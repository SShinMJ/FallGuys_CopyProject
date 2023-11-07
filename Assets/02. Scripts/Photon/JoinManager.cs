using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoinManager : MonoBehaviourPunCallbacks
{
    // 방 이름
    string roomName = "GameRoom";

    // 플레이어 정원
    [SerializeField] int maxPlayerNum = 10;
    [SerializeField] ConnectTimeCount counting;

    public TMP_Text headCountText;

    private void Start()
    {
        CreatRoom();
    }

    private void Update()
    {
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
        RoomOptions roomOptions = new RoomOptions();
        // 방을 roomName 이름으로 만든다.(이미 만들어져 있다면 Join)
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayerNum }, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        headCountText.text = (PhotonNetwork.CurrentRoom.PlayerCount).ToString() + " / " + maxPlayerNum;
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
        SceneManager.LoadScene("GameScene");
    }
}