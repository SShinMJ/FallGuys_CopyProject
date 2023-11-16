using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoManager : MonoBehaviour
{
    public string token { get; private set; }

    public void SetToken(string token)
    {
        this.token = token;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
