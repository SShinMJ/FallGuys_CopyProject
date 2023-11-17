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
        // �α��� �� �޾ƿ� ���� ���� �� �г��� ���� ������ ��´�.
        userNickname.text = "Test User";
    }

    public void ChangeNickname()
    {
        userNickname.text = inputNickname.text;

        // �������� ����� �г����� �����ϱ� ���� ������.

    }

    public void ChangeVolumn()
    {
        PlayerPrefs.SetFloat("Volume", volumnValue.value);
        volumnText.text = Mathf.Round(volumnValue.value * 100).ToString();
        bgm.volume = volumnValue.value;
    }
}
