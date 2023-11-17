using Palmmedia.ReportGenerator.Core.Common;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class UserCostumeColorUpdateManager : MonoBehaviour
{
    string url = "http://localhost:8080/api/user/costume/color";

    [SerializeField] UserInfoManager userInfo;

    class InputData
    {
        public long costumeColorNumber { get; set; }
    }

    private void Awake()
    {
        userInfo = FindObjectOfType<UserInfoManager>();
    }

    public void UpdateUserCostumeColor(long costumeColorNumber)
    {
        StartCoroutine(PutUserCostumeColor(costumeColorNumber));
    }

    IEnumerator PutUserCostumeColor(long costumeColorNumber)
    {
        var inputData = new InputData
        {
            costumeColorNumber = costumeColorNumber
        };
        string inputDataJson = JsonSerializer.ToJsonString(inputData);

        using (UnityWebRequest www = UnityWebRequest.Put(url, inputDataJson))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("X-AUTH-TOKEN", userInfo.token);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.downloadHandler.text);
            }
            else
            {
                Debug.Log("Upload complete!");
            }
        }
    }
}
