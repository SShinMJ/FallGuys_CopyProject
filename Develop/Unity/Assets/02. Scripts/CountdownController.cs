using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CountdownController : MonoBehaviour
{
    public int countdownTime = 4;

    [SerializeField] GameObject anim;

    [SerializeField] GameObject time1;   //1번
    [SerializeField] GameObject time2;   //2번
    [SerializeField] GameObject time3;   //3번
    [SerializeField] GameObject timeGO;

    // 텍스트 효과
    Animator animator;

    // 사운드 효과
    [SerializeField] AudioSource mysfx;
    [SerializeField] AudioClip startsfx;
    [SerializeField] AudioClip gosfx;

    private void Start()
    {
        time1.SetActive(false); //1번
        time2.SetActive(false); //2번
        time3.SetActive(false); //3번
        timeGO.SetActive(false);
    }

    private void OnEnable()
    {
        animator = anim.GetComponent<Animator>();

        StartCoroutine(CountdownToStart());

        Time.timeScale = 0;
    }

    //코루틴 함수 사용
    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            ChangeImage();

            // 1초 대기
            yield return new WaitForSecondsRealtime(1f);

            countdownTime--;
        }

        timeGO.SetActive(false);
        Time.timeScale = 1;

        yield return new WaitForSecondsRealtime(1f);
    }

    void ChangeImage()
    {
        if (countdownTime == 4)
        {
            time3.SetActive(true);
            animator.SetBool("Num3", true);
            mysfx.PlayOneShot(startsfx);

        }

        if (countdownTime == 3)
        {
            time2.SetActive(true);
            mysfx.PlayOneShot(startsfx);
        }

        if (countdownTime == 2)
        {
            time1.SetActive(true);
            mysfx.PlayOneShot(startsfx);
        }

        if (countdownTime == 1)
        {
            timeGO.SetActive(true);
            mysfx.PlayOneShot(gosfx);
        }

    }
}
