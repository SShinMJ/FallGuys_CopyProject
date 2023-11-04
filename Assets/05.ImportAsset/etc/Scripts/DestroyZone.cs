using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    // public ParticleSystem bounce;
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.other.gameObject);

        //bounce.Play();
        //bounce.transform.position = transform.position;
    }
}
