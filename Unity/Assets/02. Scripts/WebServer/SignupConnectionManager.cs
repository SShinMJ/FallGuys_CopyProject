using Palmmedia.ReportGenerator.Core.Common;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class SignupConnectionManager : MonoBehaviour
{
    string url = "http://localhost:8080/api/user/signup";

    [SerializeField] TMP_InputField inputId;
    [SerializeField] TMP_InputField inputPassword;
    [SerializeField] TMP_InputField inputNickname;

    [SerializeField] GameObject signupUI;
    [SerializeField] GameObject resultUI;
    [SerializeField] TextMeshProUGUI resultText;

    public void Signup()
    {
        if(inputId.text == string.Empty)
        {
            resultText.text = "���̵� �Է����ּ���!";
            resultUI.SetActive(true);
        }
        else if(inputPassword.text == string.Empty)
        {
            resultText.text = "��й�ȣ�� �Է����ּ���!";
            resultUI.SetActive(true);
        }
        else if( inputNickname.text == string.Empty)
        {
            resultText.text = "�г����� �Է����ּ���!";
            resultUI.SetActive(true);
        }
        else
        {
            StartCoroutine(SignupPost(inputId.text, inputPassword.text, inputNickname.text));
        }
    }

    class SignupData
    {
        public string userId { get; set; }
        public string userPassword { get; set; }
        public string userNickname { get; set; }
    }

    IEnumerator SignupPost(string id, string password, string nickname)
    {
        var signupData = new SignupData
        {
            userId = id,
            userPassword = password,
            userNickname = nickname
        };
        string signupDataJson = JsonSerializer.ToJsonString(signupData);

        using (UnityWebRequest www = UnityWebRequest.Post(url, signupDataJson, "application/json"))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                resultText.text = "�ߺ��� �г����̰ų� �ý��� ������\n ȸ�����Կ� �����Ͽ����ϴ�.\n �ٽ� �õ����ּ���.";
                resultUI.SetActive(true);
            }
            else
            {
                resultText.text = "ȸ�� ������ �Ϸ�Ǿ����ϴ�.";
                resultUI.SetActive(true);
                signupUI.SetActive(false);
            }
        }
    }
}
