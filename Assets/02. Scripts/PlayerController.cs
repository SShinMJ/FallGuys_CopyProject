using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float gravity = -20f;

    // ���� ��
    [SerializeField] float jumpForce = 5f;
    // ���� ���� ����
    bool isJumping = false;

    // �𵨸� ������Ʈ�� �ִϸ�����
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // �Է� �ޱ�
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        //// ������ �����ٸ�(ĳ���Ͱ� �ٴڿ� ��� �ִٸ�)
        //if (isJumping)
        //{
        //    isJumping = false;
        //}

        //// �ٴڿ� ������� ��쿣, ���� �ӵ��� ���� �����Ƿ�
        //if ()
        //{
        //    // ���� �ӵ� �ʱ�ȭ
        //    yVelocity = 0;
        //}

        //// �����̽���(����) �Է� �� ���� ���°� �ƴ϶��
        //if (Input.GetButtonDown("Jump") && !isJumping) // == Input.GetKeyDown(KeyCode.Space)
        //{
        //    yVelocity = jumpForce;
        //    isJumping = true;
        //}

        // �̵� ���� ����
        // ����(����) ��ǥ ���(������Ʈ ��(ȸ������)�� ���� ���� �����δ�)
        Vector3 dir = new Vector3(h, 0, v);
        // ���(����) ��ǥ ��� => ī�޶��� ������ ����
        dir = Camera.main.transform.TransformDirection(dir);

        // 1) �̵� �ӵ��� �÷��̾� �̵�
        //transform.position += dir * speed * Time.deltaTime;
        // 2) ĳ���� ��Ʈ�ѷ��� �÷��̾� �̵�
        transform.position += dir * playerSpeed * Time.deltaTime;
    }
}
