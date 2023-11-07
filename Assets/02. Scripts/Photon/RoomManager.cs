using Photon.Pun;
using UnityEngine;

// Photon view를 가진 플레이어를 생성한다.
public class RoomManager : MonoBehaviourPunCallbacks
{
    // Photon view 플레이어
    [SerializeField] PhotonView playerPrefab;
    // 리스폰 위치
    [SerializeField] Transform[] playerSpawnPoints;

    // 현재 접속한 클라이언트의 ActorNumber(고유 넘버) (아마 입장 순일듯)
    int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

    private void Start()
    {
        // 모든 클라이언트 중에 
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            //현재 나의 순서를 찾아,
            if (PhotonNetwork.PlayerList[i].ActorNumber == actorNumber)
            {
                // 그 순서에 해당하는 리스폰 위치에
                // Photon view를 가진 플레이어 오브젝트를 생성
                PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPoints[i].position, Quaternion.identity);
            }
        }
    }
}