using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviourPunCallbacks
{
    public float limitTime = 180;
    [SerializeField] Text textTimer;
    int min;
    float sec;
    [SerializeField] GameObject roundOver;
    [SerializeField] GameObject success;
    [SerializeField] GameObject failure;

    public int curRank { get; set; }
    public Text curRankUI;
    public Text headCountRankUI;
    [SerializeField] JoinManager joinManager;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        roundOver.SetActive(false);
        curRank = 0;
    }

    private void OnEnable()
    {
        headCountRankUI.text = " / " + joinManager.headCount.ToString();
    }

    float waitTime = 2f;
    float curretTime = 0;

    void Update()
    {
        Timer();
    }


    void Timer()
    {
        limitTime -= Time.deltaTime;

        if (limitTime >= 60f)
        {
            min = (int)limitTime / 60;
            sec = limitTime % 60;
            textTimer.text = min + " : " + (int)sec;
        }
        if (limitTime < 60f)
            textTimer.text = "<color=white>" + (int)limitTime + "</color>";
        if (limitTime < 10f)
            textTimer.text = "<color=red>" + (int)limitTime + "</color>";
        if (limitTime <= 0)
        {
            textTimer.text = "<color=red>" + "Time Over" + "</color>";
            roundOver.SetActive(true);

            curretTime += Time.deltaTime;
        }

        if (curRank == joinManager.headCount)
            limitTime = 0;
    }

    public void GameOver(bool isGoal)
    {
        if (roundOver.activeSelf == true)
        {
            if (curretTime > waitTime)
            {
                // 플레이어가 골인한 상태라면
                if (isGoal)
                {
                    if (curretTime > 3f)
                    {
                        roundOver.SetActive(false);
                        success.SetActive(true);
                    }

                }
                // 플레이어가 골인하지 못한 상태라면
                else
                {
                    if (curretTime > 3f)
                    {
                        roundOver.SetActive(false);
                        failure.SetActive(true);
                    }

                }

                if (curretTime > 5f)
                {
                    PhotonNetwork.Disconnect();
                    SceneManager.LoadScene("RewardScene");
                }
            }
        }
    }
}