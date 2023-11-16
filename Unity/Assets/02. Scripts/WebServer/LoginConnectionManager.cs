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
            resultText.text = "아이디를 입력해주세요!";
            resultUI.SetActive(true);
        }
        else if (inputPassword.text == string.Empty)
        {
            resultText.text = "비밀번호를 입력해주세요!";
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
                resultText.text = "없는 아이디이거나\n 비밀번호가 다릅니다.\n 다시 시도해주세요.";
                resultUI.SetActive(true);
            }
            else
            {
                var response = JsonUtility.FromJson<ResponseData>(www.downloadHandler.text);
                // 로그인 성공 시 토큰 정보를 저장한다.
                userInfoManager.SetToken(response.data.token);

                // 로딩씬 불러오기
                SceneManager.LoadScene("LoadingScene");
            }
        }
    }
}
