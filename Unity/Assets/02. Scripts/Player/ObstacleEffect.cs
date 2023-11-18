using UnityEngine;

public class ObstacleEffect : MonoBehaviour
{
    public Vector3 MovePos;
    public float _speed = 4f;

    // 사운드 효과
    AudioSource mysfx;
    public AudioClip bouncefx;

    void Start()
    {
        mysfx = GetComponent<AudioSource>();
        MovePos = new Vector3(1f, 1f).normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 튕겨나가기
        if (collision.gameObject.tag.Equals("Wall"))
        {
            mysfx.PlayOneShot(bouncefx);
            transform.position += MovePos * _speed * Time.deltaTime;
        }

        // 회전판의 회전에 영향을 받는다.
        if (collision.gameObject.CompareTag("Platform"))
        {
            this.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
            this.transform.parent = null;
    }
}
