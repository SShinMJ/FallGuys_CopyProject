using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoatate : CharacterController
{
    float x;
    float y;
    public float distance = 4f;
    [SerializeField] GameObject player;

    void LateUpdate()
    {
        CameraRotate();
    }

    void CameraRotate()
    {
        if (pw.IsMine)
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
            transform.position = player.transform.position - transform.rotation * reDistance;
        }
    }
}
