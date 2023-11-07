using TMPro;
using UnityEngine;

// ���� �ð�(1��)�� ������ �������� �˸���. (IsTimeOver ������ �˷�����)
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
            // ���� ����
            isTimeOver = true;
        }
    }
}
