using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public string gameVersion;
    public string nickName;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        // 1. Photon ���� ������ ����. 
        Connect();
    }

    // �����ڸ� �ڵ�� ���ٽ����� ǥ�� �����ϴ�.
    public void Connect()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.NickName = nickName;
        PhotonNetwork.ConnectUsingSettings();
    }

    // PUN�� ���ǵ� �Լ��� ���.(f12���� Ȯ�� ����)
    // ���� ���� ����� ȣ��ȴ�.
    public override void OnConnected()
    {
        base.OnConnected();
        /*        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
                Debug.Log("Callback - �����.");*/
    }

    // 2. ������ ���� ���� �� ȣ��ȴ�.
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected!!");
        base.OnConnectedToMaster();
    }

    // JoinLobby : Photon�� �����ϴ� �Լ�.
    // ��ư Ŭ�� �� ����ȴ�.
    public void JoinLobby() => PhotonNetwork.JoinLobby();

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        // Lobby ������ �̵�
        SceneManager.LoadScene("LobbyScene");
    }
}
