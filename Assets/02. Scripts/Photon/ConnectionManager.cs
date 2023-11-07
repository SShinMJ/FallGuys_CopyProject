using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    public string gameVersion = "1.0";
    public string nickName = "Test";

    // ��ư Ŭ�� �� ����ȴ�.
    public void ClickGameStartBtn()
    {
        // 1. Photon ���� ������ ����. 
        Connect();
    }

    // �����ڸ� �ڵ�� ���ٽ����� ǥ�� �����ϴ�.
    public void Connect()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.NickName = nickName;

        // AutomaticallySyncScene: ������ Ŭ���̾�Ʈ�� �Ϲ� Ŭ���̾�Ʈ���� ������ ����ȭ���� ����
        // true�� �����ϸ� ������ Ŭ�󿡼� LoadLevel()�� ������ �����ϸ� ��� Ŭ���̾�Ʈ���� �ڵ����� ������ ������ �ε�.
        PhotonNetwork.AutomaticallySyncScene = true;

        // 1�ʿ� PhotonNetwork ��������� �� �� ���� ������ ����. default�� 30
        PhotonNetwork.SendRate = 30;
        // 1�ʿ� PhotonNetwork���� �����͸� �� �� ���� ������ ����. default 10.
        // �ٸ� �÷��̾��� �̵��� ���� ���� ���̱� ���� ���� �ø���.
        PhotonNetwork.SerializationRate = 30;

        PhotonNetwork.ConnectUsingSettings();
    }

    // PUN�� ���ǵ� �Լ��� ���.(f12���� Ȯ�� ����)
    // ���� ���� ����� ȣ��ȴ�.
    public override void OnConnected()
    {
        base.OnConnected();
    }

    // 2. ������ ���� ���� �� ȣ��ȴ�.
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        // Lobby ������ �̵�
        SceneManager.LoadScene("ConnectScene");

        //JoinLobby();
    }

    //// JoinLobby : Photon�� �����ϴ� �Լ�.
    //public void JoinLobby() => PhotonNetwork.JoinLobby();

    //public override void OnJoinedLobby()
    //{
    //    base.OnJoinedLobby();

    //    // Lobby ������ �̵�
    //    SceneManager.LoadScene("ConnectScene");
    //}
}
