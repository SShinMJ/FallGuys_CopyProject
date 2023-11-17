using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [SerializeField] UserInfoManager userInfo;
    [SerializeField] RectTransform scrollContent;

    [SerializeField] GameObject player;
    [SerializeField] GameObject notification;
    [SerializeField] GameObject contents;
    [SerializeField] GameObject slotPrefab;
    [SerializeField] List<ColorCustomData> slotDatas = new List<ColorCustomData>();
    public class colorList
    {
        public GameObject slot;
        public ColorCustomData colordata;
        public bool isOwn;
    }
    public List<colorList> colorStatusList = new List<colorList>();
    public bool isSetColorList = false;

    private void Awake()
    {
        userInfo = FindObjectOfType<UserInfoManager>();
    }

    void Start()
    {
    }

    private void OnEnable()
    {
        GetColorStatus();
    }

    // ���������� �޾ƿ� �÷� ���� ���� ����Ʈ ����
    void GetColorStatus()
    {
        foreach (var slotData in slotDatas)
        {
            // �� ������ ������ �����ϰ� ������ ���� UI�� �ڽ����� �ִ´�
            GameObject slot = Instantiate(slotPrefab);
            slot.transform.SetParent(contents.transform, false);
            colorStatusList.Add(new colorList() { slot = slot, colordata = slotData, isOwn = false });

            slot.GetComponent<Image>().sprite = slotData.colorImage;
            slot.GetComponent<SelectColorManager>().player = player;
            slot.GetComponent<SelectColorManager>().colorData = slotData;
            slot.GetComponent<SelectColorManager>().notification = notification;
            slot.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = slotData.price.ToString();

            // ���� �÷��̾� �� ������ �޾ƿ� ���������� ǥ��
            if (userInfo.costumeColor.colorId == slotData.colorId)
            {
                slot.transform.GetChild(1).gameObject.SetActive(true);
                player.GetComponent<PlayerCurrentCostume>().currentMaterialSlot = slot;
            }
        }

        foreach(var color in colorStatusList)
        {
            foreach (var colorStatus in userInfo.colorStatusList)
            {
                if (color.colordata.colorId == colorStatus.colorId)
                {
                    color.isOwn = colorStatus.own;
                    if(!color.isOwn)
                    {
                        // ���� �ʿ� UI Ȱ��ȭ�ǰ� �����ϱ�.
                        color.slot.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
            }
        }

        isSetColorList = true;
    }

    // ��ũ�� �ֻ�� ��ġ
    public void setRectPosition()
    {
        scrollContent.anchoredPosition = new Vector3(scrollContent.anchoredPosition.x, 0, 0);
    }
}
