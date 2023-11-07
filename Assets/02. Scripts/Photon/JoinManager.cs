using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoinManager : MonoBehaviourPunCallbacks
{
    // �� �̸�
    string roomName = "GameRoom";

    // �÷��̾� ����
    [SerializeField] int maxPlayerNum = 10;
    [SerializeField] ConnectTimeCount counting;

    public TMP_Text headCountText;

    private void Start()
    {
        CreatRoom();
    }

    private void Update()
    {
        // ������ �� ���԰ų� ���� �ð��� �����ٸ�,
        if (PhotonNetwork.CurrentRoom != null && counting != null)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayerNum ||
                counting.IsTimeOver)
            {
                // ���� ����
                LoadScene();
            }
        }
    }

    // �κ� ���� �����.
    public void CreatRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        // ���� roomName �̸����� �����.(�̹� ������� �ִٸ� Join)
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
        Debug.Log("�� ���� ����.");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("�� ���� ����.");
    }

    void LoadScene()
    {
        // ������ ���۵ǹǷ� ���� ����
        PhotonNetwork.CurrentRoom.IsOpen = false;
        SceneManager.LoadScene("GameScene");
    }
}