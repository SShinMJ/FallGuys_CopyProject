using UnityEngine;

public class WindScroll : MonoBehaviour
{
    public float speed = 0.6f;
    Material mat;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        // 배경 스크롤
        // P =  P0 + vt
        mat.mainTextureOffset += Vector2.down * speed * Time.deltaTime;
    }
}
