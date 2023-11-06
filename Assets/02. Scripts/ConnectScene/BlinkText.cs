using System.Collections;
using UnityEngine;

public class BlinkText : MonoBehaviour
{
    void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(ShowReady());
    }

    IEnumerator ShowReady()
    {
        int count = 0;
        while (count < 100)
        {
            if (gameObject.activeInHierarchy)
            {
                Debug.Log("����!");
                gameObject.SetActive(true);
                Debug.Log("����!");
                yield return new WaitForSeconds(.5f);
                gameObject.SetActive(false);
                Debug.Log("����!");
                yield return new WaitForSeconds(.5f);
                count++;
            }
        }
    }
}
