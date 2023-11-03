using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class PlayerController : CharacterController
{
    // 수직, 수평 입력 프로퍼티
    public override float horizontal => Input.GetAxis("Horizontal");
    public override float vertical => Input.GetAxis("Vertical");
    public override float moveGain => moveValue;

    // 2초 이상 'W'키를 누를 시 달리기 시작한다.
    float currentTime;
    float limitTime = 1.0f;
    float limitValue = 3.0f;
    float moveValue = 1.0f;

    protected override void Update()
    {
        base.Update();

        if(Input.GetKey(KeyCode.W))
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

        if (Input.GetMouseButtonDown(0))
        {
            ChangeState(State.Slide);
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