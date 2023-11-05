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
        // 1. Photon 게임 서버에 연결. 
        Connect();
    }

    // 한줄자리 코드는 람다식으로 표현 가능하다.
    public void Connect()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.NickName = nickName;
        PhotonNetwork.ConnectUsingSettings();
    }

    // PUN에 정의된 함수를 사용.(f12눌러 확인 가능)
    // 포톤 서버 연결시 호출된다.
    public override void OnConnected()
    {
        base.OnConnected();
        /*        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
                Debug.Log("Callback - 연결됨.");*/
    }

    // 2. 마스터 서버 연결 시 호출된다.
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected!!");
        base.OnConnectedToMaster();
    }

    // JoinLobby : Photon이 제공하는 함수.
    // 버튼 클릭 시 실행된다.
    public void JoinLobby() => PhotonNetwork.JoinLobby();

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        // Lobby 씬으로 이동
        SceneManager.LoadScene("LobbyScene");
    }
}
