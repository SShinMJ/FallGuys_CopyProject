using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    // public ParticleSystem bounce;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = Vector3.zero;
            collision.gameObject.GetComponent<PlayerController>().ChangeStateForcely(State.Move);
        }
        else
        {
            Destroy(collision.gameObject);
        }

        //bounce.Play();
        //bounce.transform.position = transform.position;
    }
}
