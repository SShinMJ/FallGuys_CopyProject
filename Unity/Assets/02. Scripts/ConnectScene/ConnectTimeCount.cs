using TMPro;
using UnityEngine;

// 일정 시간(1분)이 지나면 지났음을 알린다. (IsTimeOver 값으로 알려진다)
public class ConnectTimeCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeCountText;

    float connectTimeLimit = 10;

    [SerializeField] OnPhotonSerializeView photonData;

    bool isTimeOver = false;
    public bool IsTimeOver
    {
        get { return isTimeOver; }
    }

    void Update()
    {
        timeCountText.text = ((int)(connectTimeLimit - photonData.CurrentTime)).ToString();

        if(connectTimeLimit - photonData.CurrentTime <= 0 )
        {
            // 게임 시작
            isTimeOver = true;
        }
    }
}
