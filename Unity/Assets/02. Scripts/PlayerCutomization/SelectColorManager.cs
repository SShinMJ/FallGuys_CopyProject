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


            // 바뀔때마다 변경된 색상값을 서버로 보내준다.
            // 커스텀 창을 나갈때 한번만 보내는 게 효율적인데, 나중에 수정해보기!
            userInfo.UpdateColorCustomData(colorData);
            dataManager.UpdateColorData();
        }
    }

    public void GetColorRequest()
    {
        if (userInfo.kudos >= colorData.price)
        {
            notification.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "정말로 구매하시겠습니까?";
            notification.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
            notification.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { notification.SetActive(false); });
            notification.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(GetColor);

        }
        else
        {
            notification.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "쿠도스 양이 부족하네요!";
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
