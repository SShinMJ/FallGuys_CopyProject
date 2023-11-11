using UnityEngine;

public class PlayerCurrentCostume : MonoBehaviour
{
    public int currentMaterialID;
    public GameObject currentMaterialSlot;

    void Awake()
    {
        CurrentColor();
    }

    // 웹서버에 현재 플레이어 색 정보 요청
    void CurrentColor()
    {
        // 임시 코드
        currentMaterialID = 0;
    }
}
