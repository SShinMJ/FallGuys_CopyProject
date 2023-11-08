using Photon.Realtime;
using UnityEngine;

public class PlayerController : CharacterController
{
    // 수직, 수평 입력 프로퍼티
    public override float horizontal 
    {
        get { return Input.GetAxis("Horizontal"); }
        set
        {
            if(pw.IsMine)
            {
                horizontal = Input.GetAxis("Horizontal");
            }
        }
    }
    public override float vertical
    {
        get { return Input.GetAxis("Vertical"); }
        set
        {
            if (pw.IsMine)
            {
                vertical = Input.GetAxis("Vertical");
            }
        }
    }
    public override float moveGain
    {
        get { return moveValue; }
        set
        {
            if (pw.IsMine)
            {
                moveGain = moveValue;
            }
        }
    }

    float currentTime;
    float limitTime = 1.0f; // 1초 이상 'W'키를 누를 시 달리기 시작한다.
    float limitValue = 3.0f;
    float moveValue = 1.0f;

    void Start()
    {
        // 메인 카메라의 타겟을 각 클라이언트로 잡히게 한다.
        if (pw.IsMine)
        {
            Camera.main.GetComponent<CameraRoatate>().targetPlayer = gameObject;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (pw.IsMine)
        {
            if (Input.GetKey(KeyCode.W))
            {
                currentTime += Time.deltaTime;
                if(currentTime > limitTime)
                {
                    if (moveValue >= limitValue)
                    {
                        moveValue = 3.0f;
                    }
                    else
                    {
                        moveValue = Mathf.Lerp(1.0f, 3.0f, (currentTime- limitTime) * 2);
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                // 자연스럽게 돌아올 수 있게 값 조절하는 부분 추가 필요~!
                moveValue = 1.0f;
                currentTime = 0;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded)
                {
                    ChangeState(State.Jump);
                }
            }

            if (Input.GetMouseButton(0))
            {
                if(_animator.GetInteger("state") != (int)State.Slide)
                {
                    ChangeStateForcely(State.Slide);
                }
            }

            if (Input.GetMouseButton(1))
            {
                if (isGrounded)
                {
                    moveValue = 2f;
                    ChangeState(State.Grab);
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (isGrounded)
                {
                    moveValue = 2f;
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                if (isGrounded)
                {
                    ChangeState(State.Move);
                }
            }
        }
    }

    public void MoveValueInit()
    {
        moveValue = 1.0f;
        currentTime = 0;
    }
}