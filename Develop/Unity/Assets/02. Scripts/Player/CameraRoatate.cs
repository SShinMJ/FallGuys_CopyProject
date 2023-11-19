using Photon.Pun;
using UnityEngine;

public class CameraRoatate : MonoBehaviour
{
    float x;
    float y;
    public float distance = 4f;
    public GameObject targetPlayer;

    void LateUpdate()
    {
        CameraRotate();
    }

    void CameraRotate()
    {
        if (targetPlayer.GetComponent<PhotonView>().IsMine)
        {
            // ���콺 �¿� �̵� ����
            x += Input.GetAxis("Mouse X");
            // ���콺 ���� �̵� ����
            y -= Input.GetAxis("Mouse Y");
            // �̵����� ���� ī�޶� �ٶ󺸴� ���� ����
            transform.rotation = Quaternion.Euler(y, x, 0);
            // ���ư� �� �ִ� ���� ����
            y = Mathf.Clamp(y, -10, 30);
            // ī�޶�� �÷��̾��� �Ÿ�����
            Vector3 reDistance = new Vector3(0f, -1.8f, distance);
            transform.position = targetPlayer.transform.position - transform.rotation * reDistance;

        }
    }
}
