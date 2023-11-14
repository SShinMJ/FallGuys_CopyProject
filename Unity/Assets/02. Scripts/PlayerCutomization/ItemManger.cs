using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManger : MonoBehaviour
{
    [SerializeField] RectTransform scrollContent;

    [SerializeField] GameObject player;
    [SerializeField] GameObject contents;
    [SerializeField] GameObject slotPrefab;
    [SerializeField] List<ColorCustomData> slotDatas = new List<ColorCustomData>();
    List<bool> colorStatus = new List<bool>();
    int playerCurrentColorId = 0;

    void Start()
    {
        GetColorStatus();

        foreach (var slotData in slotDatas)
        {
            // �� ������ ������ �����ϰ� ������ ���� UI�� �ڽ����� �ִ´�
            GameObject slot = Instantiate(slotPrefab);
            slot.transform.SetParent(contents.transform, false);

            slot.GetComponent<Image>().sprite = slotData.colorImage;
            slot.GetComponent<SelectColorManager>().colorID = slotData.colorId;
            slot.GetComponent<SelectColorManager>().player = player;
            slot.GetComponent<SelectColorManager>().material = slotData.colorMaterial;

            // ���� �÷��̾� �� ������ �޾ƿ� ���������� ǥ��
            if (player.GetComponent<PlayerCurrentCostume>().currentMaterialID == slotData.colorId)
            {
                slot.transform.GetChild(1).gameObject.SetActive(true);
                player.GetComponent<PlayerCurrentCostume>().currentMaterialSlot = slot;
            }

            // ���� ���� colorStatus�� true����(������ �ִ� ������) Ȯ�� ���� �߰� �ʿ�
            // if(colorStatus[i])  // i ���� ���� �� id�� ������ٴ� �����Ͽ� �����
            // {
            //      transform.GetChild(0).gameObject.SetActive(true);
            // }
        }
    }

    // �������� �÷��̾��� �÷� ���� ���� ��û
    void GetColorStatus()
    {

    }

    // ��ũ�� �ֻ�� ��ġ
    public void setRectPosition()
    {
        scrollContent.anchoredPosition = new Vector3(scrollContent.anchoredPosition.x, 0, 0);
    }
}
