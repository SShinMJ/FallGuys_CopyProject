using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class PlayerController : CharacterController
{
    // 수직, 수평 입력 프로퍼티
    public float horizontal => Input.GetAxis("Horizontal");
    public float vertical => Input.GetAxis("Vertical");
    // MOVE 상태에만 움직이게 해야하므로 관련 프로퍼티.
    public bool isMoveable { get; set; }
    public Vector3 move { get; set; }
    [SerializeField] private float _moveSpeed = 1.5f;

    void Update()
    {
        if (isGrounded)
        {
            animator.SetInteger("state", 0);
            isMoveable = true;

            Debug.Log(animator.GetInteger("state") + " " + animator.GetBool("isDirty") + " " + isMoveable);
        }

        // 점프키(Space)를 누르면 상태값이 바뀐다.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //animator.Get.ChangeState(2);
        }

        // 움직일 수 있을 때(점프나, 떨어지는 상태가 아닐때)
        if (isMoveable && move != new Vector3(horizontal, 0, vertical))
        {
            move = new Vector3(horizontal, 0, vertical);
        }
    }

    private void FixedUpdate()
    { 
        // 움직이는 것도 rigidbody.position이 변경되므로 FixedUpdate에 작성되야 한다.
        Move();
    }

    void Move()
    {
        _rigidbody.position += move * _moveSpeed * Time.fixedDeltaTime;
    }
}