using TMPro;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] UserKudosUpdateManager userKudosUpdateManager;

    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] TextMeshProUGUI rewardAmountText;

    UserInfoManager userInfoManager;
    int rank;
    int rewardAmount;
    bool isLoad = false;

    private void Awake()
    {
        userInfoManager = FindObjectOfType<UserInfoManager>();
    }

    private void Update()
    {
        if (!isLoad && userInfoManager != null)
        {
            SetData();

            isLoad = true;
        }
    }

    private void SetData()
    {
        rank = userInfoManager.gameRank;
        if(rank != 0 )
        {
            rankText.text = rank.ToString() + " µî";
        }
        else
        {
            rankText.text = "Å»¶ô!";
        }

        if(rank == 1)
        {
            rewardAmount = 1000;
        }
        else if(rank == 2)
        {
            rewardAmount = 800;
        }
        else if(rank == 3)
        {
            rewardAmount = 500;
        }
        else if (rank == 0)
        {
            rewardAmount = 0;
        }
        else
        {
            rewardAmount = 200;
        }

        rewardAmountText.text = rewardAmount.ToString();
        UpdateKudos();
    }

    public void UpdateKudos()
    {
        userInfoManager.SetKudos(rewardAmount);
        userKudosUpdateManager.UpdateUserKudos(rewardAmount);
    }
}
