using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    public string gameVersion = "1.0";
    public string nickName = "Test";

    // 버튼 클릭 시 실행된다.
    public void ClickGameStartBtn()
    {
        // 1. Photon 게임 서버에 연결. 
        Connect();
    }

    // 한줄자리 코드는 람다식으로 표현 가능하다.
    public void Connect()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.NickName = nickName;

        // AutomaticallySyncScene: 마스터 클라이언트와 일반 클라이언트들이 레벨을 동기화할지 결정
        // true로 설정하면 마스터 클라에서 LoadLevel()로 레벨을 변경하면 모든 클라이언트들이 자동으로 동일한 레벨을 로드.
        PhotonNetwork.AutomaticallySyncScene = true;

        // 1초에 PhotonNetwork 변경사항을 몇 번 보낼 것인지 지정. default는 30
        PhotonNetwork.SendRate = 30;
        // 1초에 PhotonNetwork에서 데이터를 몇 번 받을 것인지 지정. default 10.
        // 다른 플레이어의 이동이 끊김 없이 보이기 위해 값을 올린다.
        PhotonNetwork.SerializationRate = 30;

        PhotonNetwork.ConnectUsingSettings();
    }

    // PUN에 정의된 함수를 사용.(f12눌러 확인 가능)
    // 포톤 서버 연결시 호출된다.
    public override void OnConnected()
    {
        base.OnConnected();
    }

    // 2. 마스터 서버 연결 시 호출된다.
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        // Lobby 씬으로 이동
        SceneManager.LoadScene("ConnectScene");

        //JoinLobby();
    }

    //// JoinLobby : Photon이 제공하는 함수.
    //public void JoinLobby() => PhotonNetwork.JoinLobby();

    //public override void OnJoinedLobby()
    //{
    //    base.OnJoinedLobby();

    //    // Lobby 씬으로 이동
    //    SceneManager.LoadScene("ConnectScene");
    //}
}
