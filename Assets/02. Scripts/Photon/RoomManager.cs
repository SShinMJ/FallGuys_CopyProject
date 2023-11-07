using Photon.Pun;
using UnityEngine;

// Photon view�� ���� �÷��̾ �����Ѵ�.
public class RoomManager : MonoBehaviourPunCallbacks
{
    // Photon view �÷��̾�
    [SerializeField] PhotonView playerPrefab;
    // ������ ��ġ
    [SerializeField] Transform[] playerSpawnPoints;

    // ���� ������ Ŭ���̾�Ʈ�� ActorNumber(���� �ѹ�) (�Ƹ� ���� ���ϵ�)
    int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

    private void Start()
    {
        // ��� Ŭ���̾�Ʈ �߿� 
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            //���� ���� ������ ã��,
            if (PhotonNetwork.PlayerList[i].ActorNumber == actorNumber)
            {
                // �� ������ �ش��ϴ� ������ ��ġ��
                // Photon view�� ���� �÷��̾� ������Ʈ�� ����
                PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPoints[i].position, Quaternion.identity);
            }
        }
    }
}