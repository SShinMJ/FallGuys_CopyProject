using UnityEngine;

public class PlayerCurrentCostume : MonoBehaviour
{
    public int currentMaterialID;
    public GameObject currentMaterialSlot;

    void Awake()
    {
        CurrentColor();
    }

    // �������� ���� �÷��̾� �� ���� ��û
    void CurrentColor()
    {
        // �ӽ� �ڵ�
        currentMaterialID = 0;
    }
}
