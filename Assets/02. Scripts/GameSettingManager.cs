using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettingManager : MonoBehaviour
{
    public AudioSource bgm;

    void Start()
    {
        bgm = Camera.main.GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("Volume"))
        {
            bgm.volume = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            bgm.volume = 0.5f;
        }    
    }

    public void GotoLobbyScene()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
