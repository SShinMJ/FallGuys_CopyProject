using Palmmedia.ReportGenerator.Core.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserCostumeStatusUpdateManager : MonoBehaviour
{
    string url = "http://localhost:8080/api/user/costume/color/get";

    [SerializeField] UserInfoManager userInfo;

    class InputData
    {
        public long costumeColorNumber { get; set; }
        public int costumeColorCost { get; set; }
    }

    private void Awake()
    {
        userInfo = FindObjectOfType<UserInfoManager>();
    }

    public void UpdateUserCostumeStatus(long costumeColorNumber, int costumeColorCost)
    {
        StartCoroutine(PutUserCostumeStatus(costumeColorNumber, costumeColorCost));
    }

    IEnumerator PutUserCostumeStatus(long costumeColorNumber, int costumeColorCost)
    {
        var inputData = new InputData
        {
            costumeColorNumber = costumeColorNumber,
            costumeColorCost = costumeColorCost
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
