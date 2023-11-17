using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class UserInfoConnectionManager : MonoBehaviour
{
    string url = "http://localhost:8080/api/user";

    [SerializeField] UserInfoManager userInfo;
    [HideInInspector] public bool isSet = false;

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
        public string userId;
        public string userNickname;
        public int userKudos;
        public long userCostumeColor;
    }

    private void Awake()
    {
        userInfo = FindObjectOfType<UserInfoManager>();
    }

    public void UserInfo()
    {
        StartCoroutine(GetUserInfo());
    }

    // GET 방식
    IEnumerator GetUserInfo()
    {
        // UnityWebRequest에 내장되있는 GET 메소드를 사용한다.
        UnityWebRequest www;
        using (www = UnityWebRequest.Get(url))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("X-AUTH-TOKEN", userInfo.token);

            yield return www.SendWebRequest();  // 응답이 올때까지 대기한다.

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.result);
            }
            else
            {
                var response = JsonUtility.FromJson<ResponseData>(www.downloadHandler.text);
                userInfo.SetUserInfo(response.data.userNickname, response.data.userKudos, response.data.userCostumeColor);
                userInfo.isSetInfo = true;
            }

            isSet = true;
        }
    }
}
