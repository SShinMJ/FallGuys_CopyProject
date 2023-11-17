using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserCostumeStatusConnectionManager : MonoBehaviour
{
    string url = "http://localhost:8080/api/user/costume/color";

    [SerializeField] UserInfoManager userInfo;
    [HideInInspector] public bool isSet = false;

    [System.Serializable]
    public class ResponseData
    {
        public string httpStatus;
        public string message;
        public List<ResultData> data;
    }

    [System.Serializable]
    public class ResultData
    {
        public long colorId;
        public bool own;
    }

    private void Awake()
    {
        userInfo = FindObjectOfType<UserInfoManager>();
    }

    public void UserCostume()
    {
        StartCoroutine(GetUserInfo());
    }

    // GET 방식
    IEnumerator GetUserInfo()
    {
        UnityWebRequest www;
        using (www = UnityWebRequest.Get(url))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("X-AUTH-TOKEN", userInfo.token);

            yield return www.SendWebRequest();  // 응답이 올때까지 대기.

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.result);
            }
            else
            {
                var response = JsonUtility.FromJson<ResponseData>(www.downloadHandler.text);
                userInfo.SetUserCostumeList(response.data);
                userInfo.isSetCostumeList = true;
                Debug.Log(www.downloadHandler.text);
            }

            isSet = true;
        }
    }
}
