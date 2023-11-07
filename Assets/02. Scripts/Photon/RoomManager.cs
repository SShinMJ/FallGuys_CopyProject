using Photon.Pun;
using System.Runtime.InteropServices;
using UnityEngine;

// Photon view��  ���� �÷��̾ �����Ѵ�.
public class RoomManager : MonoBehaviourPunCallbacks
{
    // Photon view �÷��̾�
    [SerializeField] PhotonView playerPrefab;
    // ������ ��ġ
    [SerializeField] Transform[] playerSpawnPoints;

    int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

    private void Start()
    {
        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.PlayerList[i].ActorNumber == actorNumber)
            {
                Debug.Log(actorNumber);
                // Photon view��  ���� �÷��̾ ����
                PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPoints[i].position, Quaternion.identity);
            }
        }
    }
}