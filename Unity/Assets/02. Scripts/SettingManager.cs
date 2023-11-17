using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI userNickname;
    [SerializeField] TextMeshProUGUI inputNickname;
    [SerializeField] Slider volumnValue;
    [SerializeField] TextMeshProUGUI volumnText;
    [SerializeField] AudioSource bgm;

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
        userNickname.text = "Test User";
    }

    public void ChangeNickname()
    {
        userNickname.text = inputNickname.text;

        // 웹서버에 변경된 닉네임을 저장하기 위해 보낸다.

    }

    public void ChangeVolumn()
    {
        PlayerPrefs.SetFloat("Volume", volumnValue.value);
        volumnText.text = Mathf.Round(volumnValue.value * 100).ToString();
        bgm.volume = volumnValue.value;
    }
}
