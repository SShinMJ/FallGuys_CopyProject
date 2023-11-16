using Palmmedia.ReportGenerator.Core.Common;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginConnectionManager : MonoBehaviour
{
    string url = "http://localhost:8080/api/user/login";

    [SerializeField] TMP_InputField inputId;
    [SerializeField] TMP_InputField inputPassword;

    [SerializeField] GameObject resultUI;
    [SerializeField] TextMeshProUGUI resultText;

    [SerializeField] UserInfoManager userInfoManager;

    public void Login()
    {
        if (inputId.text == string.Empty)
        {
            resultText.text = "���̵� �Է����ּ���!";
            resultUI.SetActive(true);
        }
        else if (inputPassword.text == string.Empty)
        {
            resultText.text = "��й�ȣ�� �Է����ּ���!";
            resultUI.SetActive(true);
        }
        else
        {
            StartCoroutine(LoginPost(inputId.text, inputPassword.text));
        }
    }

    class LoginData
    {
        public string userId { get; set; }
        public string userPassword { get; set; }
    }

    [System.Serializable]
    public class ResponseData
    {
        public string httpStatus;
        public string message;
        public ResultData data;
    }
    
    [System.Serializable]
    public class ResultData
    {
        public string status;
        public string token;
    }

    IEnumerator LoginPost(string id, string password)
    {
        var loginData = new LoginData
        {
            userId = id,
            userPassword = password
        };
        string loginDataJson = JsonSerializer.ToJsonString(loginData);

        using (UnityWebRequest www = UnityWebRequest.Post(url, loginDataJson, "application/json"))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.result);
                Debug.Log(loginDataJson);
                resultText.text = "���� ���̵��̰ų�\n ��й�ȣ�� �ٸ��ϴ�.\n �ٽ� �õ����ּ���.";
                resultUI.SetActive(true);
            }
            else
            {
                var response = JsonUtility.FromJson<ResponseData>(www.downloadHandler.text);
                // �α��� ���� �� ��ū ������ �����Ѵ�.
                userInfoManager.SetToken(response.data.token);

                // �ε��� �ҷ�����
                SceneManager.LoadScene("LoadingScene");
            }
        }
    }
}
