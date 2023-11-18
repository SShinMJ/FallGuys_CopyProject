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
            resultText.text = "아이디를 입력해주세요!";
            resultUI.SetActive(true);
        }
        else if(inputPassword.text == string.Empty)
        {
            resultText.text = "비밀번호를 입력해주세요!";
            resultUI.SetActive(true);
        }
        else if( inputNickname.text == string.Empty)
        {
            resultText.text = "닉네임을 입력해주세요!";
            resultUI.SetActive(true);
        }
        else
        {
            StartCoroutine(SignupPost(inputId.text, inputPassword.text, inputNickname.text));
        }
    }

    [System.Serializable]
    class SignupData
    {
        public string userId;
        public string userPassword;
        public string userNickname;
    }

    IEnumerator SignupPost(string id, string password, string nickname)
    {
        var signupData = new SignupData
        {
            userId = id,
            userPassword = password,
            userNickname = nickname
        };
        string signupDataJson = JsonUtility.ToJson(signupData);

        using (UnityWebRequest www = UnityWebRequest.Post(url, signupDataJson, "application/json"))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                resultText.text = "중복된 닉네임이거나 시스템 오류로\n 회원가입에 실패하였습니다.\n 다시 시도해주세요.";
                resultUI.SetActive(true);
            }
            else
            {
                resultText.text = "회원 가입이 완료되었습니다.";
                resultUI.SetActive(true);
                signupUI.SetActive(false);
            }
        }
    }
}
