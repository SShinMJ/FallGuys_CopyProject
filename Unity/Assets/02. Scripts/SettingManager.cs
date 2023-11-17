using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] UserInfoManager userInfo;

    [SerializeField] TextMeshProUGUI userNickname;
    [SerializeField] TextMeshProUGUI inputNickname;
    [SerializeField] Slider volumnValue;
    [SerializeField] TextMeshProUGUI volumnText;
    [SerializeField] AudioSource bgm;

    private void Awake()
    {
        userInfo = FindObjectOfType<UserInfoManager>();
    }

    void Start()
    {
        GetUserNickname();

        if (PlayerPrefs.HasKey("Volume"))
        {
            volumnValue.value = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            volumnValue.value = 0.5f;
        }

        volumnText.text = Mathf.Round(volumnValue.value*100).ToString();
    }

    public void GetUserNickname()
    {
        // 로그인 시 받아온 유저 정보 중 닉네임 값을 가져와 담는다.
        if(userInfo.nickname == null) 
            userNickname.text = "Test User";
        else
            userNickname.text = userInfo.nickname;
    }

    public void ChangeNickname()
    {
        userNickname.text = inputNickname.text;

        userInfo.UpdateNickname(userNickname.text);
    }

    public void ChangeVolumn()
    {
        PlayerPrefs.SetFloat("Volume", volumnValue.value);
        volumnText.text = Mathf.Round(volumnValue.value * 100).ToString();
        bgm.volume = volumnValue.value;
    }
}
