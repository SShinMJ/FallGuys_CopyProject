using TMPro;
using UnityEngine;

public class ConnectTimeCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeCountText;

    float currenTime = 0;
    float connectTimeLimit = 60;

    void Update()
    {
        currenTime += Time.deltaTime;

        timeCountText.text = ((int)(connectTimeLimit - currenTime)).ToString();

        if(connectTimeLimit - currenTime == 0 )
        {
            // 게임 시작
        }
    }
}
