using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaitingCharacterMove : MonoBehaviour
{
    public GameObject destPos;
    float speed = 1.8f;
    Vector3 dir;

    void Start()
    {
    }

    void Update()
    {
        dir = destPos.transform.position - transform.position;
        transform.position += dir * speed * Time.deltaTime;
    }
}
