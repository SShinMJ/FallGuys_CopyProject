using System.Collections.Generic;
using UnityEngine;

public class UserInfoManager : MonoBehaviour
{
    public string token { get; private set; }
    public string nickname { get; private set; }
    public int kudos { get; private set; }
    public ColorCustomData costumeColor { get; private set; }

    [SerializeField] List<ColorCustomData> colorList = new List<ColorCustomData>();

    public List<UserCostumeStatusConnectionManager.ResultData> colorStatusList { get; private set; }

    [HideInInspector] public bool isSetInfo = false;
    [HideInInspector] public bool isSetCostumeList = false;

    // 나중에 게임 메니저로 빼기
    public int gameRank { get; private set; }

    private void Awake()
    {
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
                this.costumeColor = data;
        }
    }

    public void SetUserCostumeList(List<UserCostumeStatusConnectionManager.ResultData> colorStatusList)
    {
        this.colorStatusList = colorStatusList;
    }

    public void UpdateNickname(string nickname)
    {
        this.nickname = nickname;
    }

    public void UpdateColorCustomData(ColorCustomData colorData)
    {
        this.costumeColor = colorData;
    }

    public void SetKudos(int amount)
    {
        this.kudos += amount;
    }

    public void SetGameRank(int rank)
    {
        this.gameRank = rank;
    }
}
