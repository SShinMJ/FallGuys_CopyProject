using System.Collections.Generic;
using UnityEngine;

public class UserInfoManager : MonoBehaviour
{
    public static UserInfoManager Instance;

    public string token { get; private set; }
    public string nickname { get; private set; }
    public int kudos { get; private set; }
    public Material costumeColor { get; private set; }

    [SerializeField] List<ColorCustomData> colorList = new List<ColorCustomData>();

    public List<UserCostumeStatusConnectionManager.ResultData> colorStatusList { get; private set; }

    [HideInInspector] public bool isSetInfo = false;
    [HideInInspector] public bool isSetCostumeList = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        var obj = FindObjectsOfType<UserInfoManager>();

        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetToken(string token)
    {
        this.token = token;
    }

    public void SetUserInfo(string nickname, int kudos, long costumeColorNumber)
    {
        this.nickname = nickname;
        this.kudos = kudos;
        foreach (ColorCustomData data in colorList)
        {
            if (data.colorId == costumeColorNumber)
                this.costumeColor = data.colorMaterial;
        }
        //Debug.Log(this.nickname + ", " + this.kudos + ", " + this.costumeColor.mainTexture.name);
    }

    public void SetUserCostumeList(List<UserCostumeStatusConnectionManager.ResultData> colorStatusList)
    {
        this.colorStatusList = colorStatusList;
    }

    public void PutNickname(string nickname)
    {
        this.nickname = nickname;
    }
}
