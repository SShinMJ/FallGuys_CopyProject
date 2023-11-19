using Photon.Pun;
using UnityEngine;

// ConnectionScene에서 남은 시간 초를 동기화한다.
public class OnPhotonSerializeView : MonoBehaviour, IPunObservable
{
    // 게임 시작까지 남은 초를 담는 변수가 전체 클라이언트에게 공유된다.
    float currentTime = 0;
    public float CurrentTime
    {
        get { return currentTime; }
    }

    void Update()
    {
        currentTime += Time.deltaTime;
    }

    // IPunObservable 상속 시 꼭 구현해야 하는 것
    // - 데이터를 네트워크 사용자 간에 보내고 받고 하게 하는 콜백 함수
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // stream - 데이터를 주고 받는 통로 
        // 내가 마스터 클라이언트라면
        if (PhotonNetwork.IsMasterClient)
        {
            // 이 방안에 있는 모든 사용자에게 브로드캐스트 
            stream.SendNext(currentTime);
        }
        // 내가 클라이언트라면 
        else
        {
            // 데이터를 받아온다. 타입캐스팅 필요.
            this.currentTime = (float)stream.ReceiveNext();
        }
    }
}
