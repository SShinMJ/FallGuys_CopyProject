using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float gravity = -20f;

    // 점프 힘
    [SerializeField] float jumpForce = 5f;
    // 점프 상태 변수
    bool isJumping = false;

    // 모델링 오브젝트의 애니메이터
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 입력 받기
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        //// 점프가 끝났다면(캐릭터가 바닥에 닿아 있다면)
        //if (isJumping)
        //{
        //    isJumping = false;
        //}

        //// 바닥에 닿아있을 경우엔, 수직 속도를 받지 않으므로
        //if ()
        //{
        //    // 수직 속도 초기화
        //    yVelocity = 0;
        //}

        //// 스페이스바(점프) 입력 시 점프 상태가 아니라면
        //if (Input.GetButtonDown("Jump") && !isJumping) // == Input.GetKeyDown(KeyCode.Space)
        //{
        //    yVelocity = jumpForce;
        //    isJumping = true;
        //}

        // 이동 방향 설정
        // 절대(월드) 좌표 방식(오브젝트 축(회전방향)과 관계 없이 움직인다)
        Vector3 dir = new Vector3(h, 0, v);
        // 상대(로컬) 좌표 방식 => 카메라의 축으로 설정
        dir = Camera.main.transform.TransformDirection(dir);

        // 1) 이동 속도로 플레이어 이동
        //transform.position += dir * speed * Time.deltaTime;
        // 2) 캐릭터 컨트롤러로 플레이어 이동
        transform.position += dir * playerSpeed * Time.deltaTime;
    }
}
