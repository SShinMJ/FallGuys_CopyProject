using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public void GotoLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
