using Palmmedia.ReportGenerator.Core.Common;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class UserNicknameUpdateManager : MonoBehaviour
{
    string url = "http://localhost:8080/api/user/nickname";

    [SerializeField] UserInfoManager userInfo;

    class InputData
    {
        public string userNickname { get; set; }
    }

    private void Awake()
    {
        userInfo = FindObjectOfType<UserInfoManager>();
    }

    public void UpdateUserNickname(string nickname)
    {
        StartCoroutine(PutUserNickname(nickname));
    }

    IEnumerator PutUserNickname(string nickname)
    {
        var inputData = new InputData
        {
            userNickname = nickname
        };
        string inputDataJson = JsonSerializer.ToJsonString(inputData);

        using (UnityWebRequest www = UnityWebRequest.Put(url, inputDataJson))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("X-AUTH-TOKEN", userInfo.token);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Upload complete!");
            }
        }
    }
}
