using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// ù ���� Ŭ���̾�Ʈ�� ���� ����� �����ϰ�,
// ���� ����(1��, 10��)���� Ŭ���̾�Ʈ���� ����(Join)�� �޴´�.
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
        // ���� ������ �� UI �ؽ�Ʈ�� ���
        if (headCountText != null && PhotonNetwork.CurrentRoom != null)
        {
            headCountText.text = (PhotonNetwork.CurrentRoom.PlayerCount).ToString() + " / " + maxPlayerNum;
        }

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
        // ���� roomName �̸����� �����.(�̹� ������� �ִٸ� Join)
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayerNum }, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
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