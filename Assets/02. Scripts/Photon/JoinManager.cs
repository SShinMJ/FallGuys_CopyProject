using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// ù ���� Ŭ���̾�Ʈ�� ���� ����� �����ϰ�,
// ���� ����(1��, 10��)���� Ŭ���̾�Ʈ���� ����(Join)�� �޴´�.
public class JoinManager : MonoBehaviourPunCallbacks
{
    // ConnectScene Objects
    [SerializeField] GameObject connectScene;

    // �� �̸�
    string roomName = "GameRoom";

    // �÷��̾� ����
    [SerializeField] int maxPlayerNum = 10;
    [SerializeField] ConnectTimeCount counting;

    public TMP_Text headCountText;

    // �÷��̾� ������Ʈ
    GameObject player;

    // Photon view �÷��̾�
    [SerializeField] PhotonView playerPrefab;
    // ������ ��ġ
    [SerializeField] Transform[] playerSpawnPoints;
    // �� ������Ʈ
    [SerializeField] GameObject map;

    // ���� ������ Ŭ���̾�Ʈ�� ActorNumber(���� �ѹ�) (�Ƹ� ���� ���ϵ�)
    int actorNumber;

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

        connectScene.SetActive(false);
        map.SetActive(true);
        player.SetActive(true);
        Camera.main.GetComponent<CameraRoatate>().enabled = true;

        //SceneManager.LoadScene("GameScene");
    }
}