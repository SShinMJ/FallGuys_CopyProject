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

        // 둘 다 통신이 완료됐다면,
        if (userInfoConnectionManager.isSet && userCostumeStatusConnection.isSet)
        {
            NoticeView();
        }

        if (itemManager.isSetColorList && currentTime > 3)
            loadComplete();
    }

    void NoticeView()
    {
        // 한 번만 실행되게 초기화
        userInfoConnectionManager.isSet = false;
        userCostumeStatusConnection.isSet = false;

        // 유저 정보나 코스튬 정보를 가져오는 데 실패했다면
        if (!userInfo.isSetInfo || !userInfo.isSetCostumeList)
        {
            // 로그인 씬으로 빽
            resultText.text = "사용자 데이터를 가져오는데\n 실패했습니다.\n 다시 시도해주세요.";
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
