using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    [SerializeField] UserInfoManager userInfo;
    [SerializeField] UserInfoConnectionManager userInfoConnectionManager;
    [SerializeField] UserCostumeStatusConnectionManager userCostumeStatusConnection;
    [SerializeField] UserNicknameUpdateManager userNicknameUpdateManager;
    [SerializeField] UserCostumeColorUpdateManager userCostumeColorUpdateManager;
    [SerializeField] UserCostumeStatusUpdateManager UserCostumeStatusUpdateManager;

    [SerializeField] GameObject loadingCanvas;
    [SerializeField] GameObject resultUI;
    [SerializeField] TextMeshProUGUI resultText;

    [SerializeField] TextMeshProUGUI KudosText;
    [SerializeField] TextMeshProUGUI NicknameText;
    [SerializeField] GameObject player;

    [SerializeField] ItemManager itemManager;

    float currentTime = 0;

    private void Awake()
    {
        userInfo = FindObjectOfType<UserInfoManager>();
    }

    private void Start()
    {
        userInfoConnectionManager.UserInfo();
        userCostumeStatusConnection.UserCostume();
    }

    private void Update()
    {
        // �� �� ����� �Ϸ�ƴٸ�,
        if (userInfoConnectionManager.isSet && userCostumeStatusConnection.isSet)
        {
            NoticeView();
        }

        if (itemManager.isSetColorList && currentTime > 3)
            loadComplete();
        else
            currentTime += Time.deltaTime;
    }

    void NoticeView()
    {
        // �� ���� ����ǰ� �ʱ�ȭ
        userInfoConnectionManager.isSet = false;
        userCostumeStatusConnection.isSet = false;

        // ���� ������ �ڽ�Ƭ ������ �������� �� �����ߴٸ�
        if (!userInfo.isSetInfo || !userInfo.isSetCostumeList)
        {
            // �α��� ������ ��
            resultText.text = "����� �����͸� �������µ�\n �����߽��ϴ�.\n �ٽ� �õ����ּ���.";
            resultUI.SetActive(true);
        }
        else
        {
            InsertData();
            itemManager.enabled = true;
        }
    }

    public void BackToLoginScene()
    {
        SceneManager.LoadScene("LoginScene");
    }

    public void InsertData()
    {
        KudosText.text = userInfo.kudos.ToString();
        NicknameText.text = userInfo.nickname;
        player.GetComponent<SkinnedMeshRenderer>().material = userInfo.costumeColor.colorMaterial;
    }

    public void loadComplete()
    {
        loadingCanvas.SetActive(false);
    }

    public void UpdateNickname()
    {
        NicknameText.text = userInfo.nickname;
        userNicknameUpdateManager.UpdateUserNickname(userInfo.nickname);
    }

    public void UpdateColorData()
    {
        userCostumeColorUpdateManager.UpdateUserCostumeColor(userInfo.costumeColor.colorId);
    }

    public void GetColorData(ColorCustomData colorData)
    {
        userInfo.SetKudos(-colorData.price);
        KudosText.text = userInfo.kudos.ToString();
        UserCostumeStatusUpdateManager.UpdateUserCostumeStatus(colorData.colorId, colorData.price);
    }
}
