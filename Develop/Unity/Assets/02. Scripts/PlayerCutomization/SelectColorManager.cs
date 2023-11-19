using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SelectColorManager : MonoBehaviour
{
    public GameObject player;
    public ColorCustomData colorData;

    [SerializeField] UserInfoManager userInfo;
    DataManager dataManager;

    public GameObject notification;


    private void Awake()
    {
        userInfo = FindObjectOfType<UserInfoManager>();
        dataManager = FindObjectOfType<DataManager>();
    }

    public void ChangePlayerColor()
    {
        if (!transform.GetChild(0).gameObject.activeSelf)
        {
            player.GetComponent<Renderer>().material = colorData.colorMaterial;
            transform.GetChild(1).gameObject.SetActive(true);
            player.GetComponent<PlayerCurrentCostume>().currentMaterialID = colorData.colorId;
            player.GetComponent<PlayerCurrentCostume>().currentMaterialSlot.transform.GetChild(1).gameObject.SetActive(false);
            player.GetComponent<PlayerCurrentCostume>().currentMaterialSlot = gameObject;


            // �ٲ𶧸��� ����� ������ ������ �����ش�.
            // Ŀ���� â�� ������ �ѹ��� ������ �� ȿ�����ε�, ���߿� �����غ���!
            userInfo.UpdateColorCustomData(colorData);
            dataManager.UpdateColorData();
        }
    }

    public void GetColorRequest()
    {
        if (userInfo.kudos >= colorData.price)
        {
            notification.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "������ �����Ͻðڽ��ϱ�?";
            notification.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
            notification.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { notification.SetActive(false); });
            notification.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(GetColor);

        }
        else
        {
            notification.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "���� ���� �����ϳ׿�!";
            notification.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
            notification.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { notification.SetActive(false); });
            notification.transform.GetChild(0).GetChild(1).GetChild(1).gameObject.SetActive(false);
        }

        notification.SetActive(true);
    }

    public void GetColor()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        dataManager.GetColorData(colorData);
    }
}
