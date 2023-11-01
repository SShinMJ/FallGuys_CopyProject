using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class PlayerController : CharacterController
{
    // ����, ���� �Է� ������Ƽ
    public float horizontal => Input.GetAxis("Horizontal");
    public float vertical => Input.GetAxis("Vertical");
    // MOVE ���¿��� �����̰� �ؾ��ϹǷ� ���� ������Ƽ.
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

        // ����Ű(Space)�� ������ ���°��� �ٲ��.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //animator.Get.ChangeState(2);
        }

        // ������ �� ���� ��(������, �������� ���°� �ƴҶ�)
        if (isMoveable && move != new Vector3(horizontal, 0, vertical))
        {
            move = new Vector3(horizontal, 0, vertical);
        }
    }

    private void FixedUpdate()
    { 
        // �����̴� �͵� rigidbody.position�� ����ǹǷ� FixedUpdate�� �ۼ��Ǿ� �Ѵ�.
        Move();
    }

    void Move()
    {
        _rigidbody.position += move * _moveSpeed * Time.fixedDeltaTime;
    }
}