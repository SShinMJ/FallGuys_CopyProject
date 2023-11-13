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
            // 새 아이템 슬롯을 생성하고 아이템 정렬 UI의 자식으로 넣는다
            GameObject slot = Instantiate(slotPrefab);
            slot.transform.SetParent(contents.transform, false);

            slot.GetComponent<Image>().sprite = slotData.colorImage;
            slot.GetComponent<SelectColorManager>().colorID = slotData.colorId;
            slot.GetComponent<SelectColorManager>().player = player;
            slot.GetComponent<SelectColorManager>().material = slotData.colorMaterial;

            // 현재 플레이어 색 정보를 받아와 선택중임을 표시
            if (player.GetComponent<PlayerCurrentCostume>().currentMaterialID == slotData.colorId)
            {
                slot.transform.GetChild(1).gameObject.SetActive(true);
                player.GetComponent<PlayerCurrentCostume>().currentMaterialSlot = slot;
            }

            // 현재 색의 colorStatus가 true인지(가지고 있는 색인지) 확인 절차 추가 필요
            // if(colorStatus[i])  // i 값을 현재 색 id와 맞춰줬다는 가정하에 써야함
            // {
            //      transform.GetChild(0).gameObject.SetActive(true);
            // }
        }
    }

    // 웹서버에 플레이어의 컬러 소유 정보 요청
    void GetColorStatus()
    {

    }

    // 스크롤 최상단 위치
    public void setRectPosition()
    {
        scrollContent.anchoredPosition = new Vector3(scrollContent.anchoredPosition.x, 0, 0);
    }
}
