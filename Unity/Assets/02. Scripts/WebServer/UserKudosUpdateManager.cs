using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class UserKudosUpdateManager : MonoBehaviour
{
    string url = "http://localhost:8080/api/user/kudos/get";

    [SerializeField] UserInfoManager userInfo;

    [System.Serializable]
    class InputData
    {
        public int getKudos;
    }

    private void Awake()
    {
        userInfo = FindObjectOfType<UserInfoManager>();
    }

    public void UpdateUserKudos(int amount)
    {
        StartCoroutine(PutUserKudos(amount));
    }

    IEnumerator PutUserKudos(int amount)
    {
        var inputData = new InputData
        {
            getKudos = amount
        };
        string inputDataJson = JsonUtility.ToJson(inputData);

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
