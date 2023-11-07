using Photon.Pun;
using System.Runtime.InteropServices;
using UnityEngine;

// Photon view를  가진 플레이어를 생성한다.
public class RoomManager : MonoBehaviourPunCallbacks
{
    // Photon view 플레이어
    [SerializeField] PhotonView playerPrefab;
    // 리스폰 위치
    [SerializeField] Transform[] playerSpawnPoints;

    int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

    private void Start()
    {
        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.PlayerList[i].ActorNumber == actorNumber)
            {
                Debug.Log(actorNumber);
                // Photon view를  가진 플레이어를 생성
                PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPoints[i].position, Quaternion.identity);
            }
        }
    }
}