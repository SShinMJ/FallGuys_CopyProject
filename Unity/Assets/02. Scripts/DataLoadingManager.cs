using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataLoadingManager : MonoBehaviour
{
    [SerializeField] UserInfoManager userInfo;
    [SerializeField] UserInfoConnectionManager userInfoConnectionManager;
    [SerializeField] UserCostumeStatusConnectionManager userCostumeStatusConnection;

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
        currentTime += Time.deltaTime;

        // �� �� ����� �Ϸ�ƴٸ�,
        if (userInfoConnectionManager.isSet && userCostumeStatusConnection.isSet)
        {
            NoticeView();
        }

        if (itemManager.isSetColorList && currentTime > 3)
            loadComplete();
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
        player.GetComponent<SkinnedMeshRenderer>().material.parent = userInfo.costumeColor;
    }

    public void loadComplete()
    {
        gameObject.SetActive(false);
    }
}
