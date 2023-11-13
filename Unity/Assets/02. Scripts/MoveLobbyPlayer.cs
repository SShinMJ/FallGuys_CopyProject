using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveLobbyPlayer : MonoBehaviour
{
    [SerializeField] GameObject centerDes;
    [SerializeField] GameObject LeftDes;

    Vector3 originPos;
    Quaternion originRot;
    Vector3 desPos;
    Quaternion desRot;
    float countTime = 0;

    void Start()
    {
        originPos = transform.position;
        originRot = transform.rotation;
        desPos = transform.position;
        desRot = transform.rotation;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, desPos, 0.3f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desRot, 0.8f);
    }

    public void MoveToCenterObject()
    {
        originPos = transform.position;
        originRot = transform.rotation;
        desPos = centerDes.transform.position;
        desRot = centerDes.transform.rotation;
    }

    public void MoveToLeftObject()
    {
        originPos = transform.position;
        originRot = transform.rotation;
        desPos = LeftDes.transform.position;
        desRot = LeftDes.transform.rotation;
    }
}
