using UnityEngine;

public class PlayerController : CharacterController
{
    [HideInInspector] public Vector3 respawnPosition;

    [HideInInspector] public GameObject destination;
    bool isGoal = false;

    // ����, ���� �Է� ������Ƽ
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
    float limitTime = 1.0f; // 1�� �̻� 'W'Ű�� ���� �� �޸��� �����Ѵ�.
    float limitValue = 3.0f;
    float moveValue = 1.0f;

    // ���� ȿ��
    AudioSource playerAudio;
    [SerializeField] AudioClip jumpSFX;

    void Start()
    {
        respawnPosition = transform.position;
        playerAudio = GetComponent<AudioSource>();

        // ���� ī�޶��� Ÿ���� �� Ŭ���̾�Ʈ�� ������ �Ѵ�.
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
                // �ڿ������� ���ƿ� �� �ְ� �� �����ϴ� �κ� �߰� �ʿ�~!
                moveValue = 1.0f;
                currentTime = 0;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded && _animator.GetInteger("state") != (int)State.Slide)
                {
                    ChangeState(State.Jump);
                    playerAudio.PlayOneShot(jumpSFX);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if(_animator.GetInteger("state") != (int)State.Slide)
                {
                    ChangeStateForcely(State.Slide);
                    playerAudio.PlayOneShot(jumpSFX);
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

        if (pw.IsMine)
        {
            if (UIManager.Instance.limitTime <= 0)
            {
                UIManager.Instance.GameOver(isGoal);
            }
        }
    }

    public void MoveValueInit()
    {
        moveValue = 1.0f;
        currentTime = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (pw.IsMine)
        {
            if (collision.gameObject.tag == "Destination")
            {
                UIManager.Instance.curRank++;
                UIManager.Instance.curRankUI.text = UIManager.Instance.curRank.ToString();
                userInfoManager.SetGameRank(UIManager.Instance.curRank);
                isGoal = true;
            }
        }
    }
}