using Photon.Pun;
using UnityEngine;

// ConnectionScene���� ���� �ð� �ʸ� ����ȭ�Ѵ�.
public class OnPhotonSerializeView : MonoBehaviour, IPunObservable
{
    // ���� ���۱��� ���� �ʸ� ��� ������ ��ü Ŭ���̾�Ʈ���� �����ȴ�.
    float currentTime = 0;
    public float CurrentTime
    {
        get { return currentTime; }
    }

    void Update()
    {
        currentTime += Time.deltaTime;
    }

    // IPunObservable ��� �� �� �����ؾ� �ϴ� ��
    // - �����͸� ��Ʈ��ũ ����� ���� ������ �ް� �ϰ� �ϴ� �ݹ� �Լ�
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // stream - �����͸� �ְ� �޴� ��� 
        // ���� ������ Ŭ���̾�Ʈ���
        if (PhotonNetwork.IsMasterClient)
        {
            // �� ��ȿ� �ִ� ��� ����ڿ��� ��ε�ĳ��Ʈ 
            stream.SendNext(currentTime);
        }
        // ���� Ŭ���̾�Ʈ��� 
        else
        {
            // �����͸� �޾ƿ´�. Ÿ��ĳ���� �ʿ�.
            this.currentTime = (float)stream.ReceiveNext();
        }
    }
}
