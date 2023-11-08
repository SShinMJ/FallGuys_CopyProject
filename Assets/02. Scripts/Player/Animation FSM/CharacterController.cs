using Photon.Pun;
using System;
using UnityEngine;

// abstract 이므로, 각 캐릭터(Player 등)이 상속받아 사용하면 된다.
// == Animator의 Animation Blending을 고려한 설계이다.
public abstract class CharacterController : MonoBehaviour
{
    // Photon
    public PhotonView pw;

    // 각 캐릭터에 따라 다른 입력을 받아오므로,
    public virtual float horizontal { get; set; }
    public virtual float vertical { get; set; }
    // 애니메이션 블랜딩을 위한 가중치
    public virtual float moveGain { get; set; }
    [SerializeField] private float _moveSpeed = 1.5f;

    public Vector3 move;
    public bool isMovable
    {
        get
        {
            if (states[0] != State.Move)
            {
                return false;
            }

            return true;
        }
    }

    public State[] states;
    public State next;
    protected Animator _animator;
    public StateLayerMaskData stateLayerMaskData;
    public bool isGrounded => DetectGround();

    [SerializeField] private float _groundDetectRadius;
    private Vector3 _inertia;  // 관성
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _slope = 45.0f;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        pw = GetComponent<PhotonView>();

        // 내가 원하는 StateMachineBehaviour 데이터들을 읽어올 수 있다.(배열 리턴)
        // StateBase 스크립트가 포함된 애니메이션들이 불러와진다.
        StateBase[] behaviours = _animator.GetBehaviours<StateBase>();
        for (int i = 0; i < behaviours.Length; i++)
        {
            // 초기화
            behaviours[i].Init(this, stateLayerMaskData);
        }

        Array layers = Enum.GetValues(typeof(AnimatorLayers));
        states = new State[layers.Length - 1];
        ChangeStateForcely(State.Move);
    }

    protected virtual void Update()
    {
        if (pw.IsMine)
        {
            if (isMovable)
            {
                move = new Vector3(horizontal, 0.0f, vertical).normalized * moveGain;
            }
            _animator.SetFloat("h", horizontal * moveGain);
            _animator.SetFloat("v", vertical * moveGain);
        } 
    }

    protected virtual void FixedUpdate()
    {
        if(pw.IsMine)
        {
            // 땅에 닿았는 지 여부에 따른 관성
            if (DetectGround())
            {
                _inertia.y = 0.0f;
            }
            else
            {
                _inertia.y += Physics.gravity.y * Time.fixedDeltaTime;
            }

            // 관성 값이 생기면 위치를 변화시킨다.
            if (_inertia.magnitude > 0.0f)
                transform.Translate(_inertia * Time.fixedDeltaTime);

            // 카메라 방향으로 이동 방향 설정
            Vector3 offset = Camera.main.transform.forward;
            offset.y = 0;
            transform.LookAt(transform.position + offset);

            // 이동
            Vector3 expected = transform.position
                               + Quaternion.LookRotation(transform.forward, Vector3.up) * move * _moveSpeed * Time.fixedDeltaTime;

            transform.position = expected;
        }
        //else
        //{
        //    // 상대 플레이어의 위치, 회전값을 적용한다.
        //    transform.position = receivedPos;
        //    transform.rotation = receivedRot;

        //    Vector3 dir = new Vector3(receivedH, 0, receivedV);
        //    dir = Camera.main.transform.TransformDirection(dir);

        //    _animator.SetInteger("state", animState);
        //}
    }

    // 땅인지 검사하는 함수
    private bool DetectGround()
    {
        Collider[] cols
            = Physics.OverlapSphere(transform.position, _groundDetectRadius, _groundMask);
        return cols.Length > 0;
    }

    // 대상과 현재 상태값이 같은 지 확인
    public bool IsInState(State state)
    {
        int layerIndex = 0;
        foreach (AnimatorLayers layer in Enum.GetValues(typeof(AnimatorLayers)))
        {
            if (layer == AnimatorLayers.None)
                continue;

            if ((layer & stateLayerMaskData.animatorLayerPairs[state]) > 0)
            {
                if (states[layerIndex] == state)
                    return true;
            }
        }

        return false;
    }

    // 애니메이션 상태 변환
    public void ChangeState(State newState)
    {
        _animator.SetInteger("state", (int)newState);
        next = newState;
        int layerIndex = 0;
        foreach (AnimatorLayers layer in Enum.GetValues(typeof(AnimatorLayers)))
        {
            if (layer == AnimatorLayers.None)
                continue;

            if ((layer & stateLayerMaskData.animatorLayerPairs[newState]) > 0)
            {
                // 상태값이 바뀌어야 한다면 바뀌는 동안 다른 애니메이션이 실행되지 않게
                // dirty 값 변경.
                if (states[layerIndex] != newState)
                    _animator.SetBool($"dirty{layer}", true);

                _animator.SetLayerWeight(layerIndex, 1.0f);
            }
            else
            {
                _animator.SetLayerWeight(layerIndex, 0.0f);
            }
            layerIndex++;
        }
    }

    // 이전 상태와 관계없이 강제로 해당 애니메이션 상태로 바꾸기.
    public void ChangeStateForcely(State newState)
    {
        _animator.SetInteger("state", (int)newState);
        next = newState;
        int layerIndex = 0;
        foreach (AnimatorLayers layer in Enum.GetValues(typeof(AnimatorLayers)))
        {
            if (layer == AnimatorLayers.None)
                continue;

            if ((layer & stateLayerMaskData.animatorLayerPairs[newState]) > 0)
            {
                _animator.SetBool($"dirty{layer}", true);
                _animator.SetLayerWeight(layerIndex, 1.0f);
            }
            else
            {
                _animator.SetLayerWeight(layerIndex, 0.0f);
            }
            layerIndex++;
        }
    }

    //float receivedH;
    //float receivedV;
    //Vector3 receivedPos;
    //Quaternion receivedRot;
    //int animState;
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    // 상대쪽에 있는 현재 플레이어 위치, 회전 데이터를 상대 플레이어에게 보낸다.
    //    if (stream.IsReading)
    //    {
    //        stream.SendNext(transform.position);
    //        stream.SendNext(transform.rotation);
    //        stream.SendNext(horizontal);
    //        stream.SendNext(vertical);
    //        stream.SendNext(_animator.GetInteger("state"));
    //    }
    //    // stream.IsWriting. 현재 플레이어쪽에 있는 상대 플레이어의 위치, 회전 데이터를 받는다.
    //    else
    //    {
    //        receivedPos = (Vector3)stream.ReceiveNext();
    //        receivedRot = (Quaternion)stream.ReceiveNext();
    //        horizontal = (float)stream.ReceiveNext();
    //        vertical = (float)stream.ReceiveNext();
    //        animState = (int)stream.ReceiveNext();
    //    }
    //}
}