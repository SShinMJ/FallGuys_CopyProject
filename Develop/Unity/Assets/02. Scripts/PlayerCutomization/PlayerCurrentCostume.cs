using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrentCostume : MonoBehaviour
{
    UserInfoManager userInfoManager;

    [SerializeField] CharacterController playerPhoton = null;

    public int currentMaterialID;
    public GameObject currentMaterialSlot;

    public List<ColorCustomData> colorStatusList = new List<ColorCustomData>();

    bool isDone = false;

    private void Start()
    {
        userInfoManager = FindObjectOfType<UserInfoManager>();
    }

    private void Update()
    {
        if (gameObject.activeSelf && !isDone)
        {
            if (playerPhoton != null && playerPhoton.pw != null)
            {
                if (userInfoManager != null)
                {
                    if (playerPhoton.pw.IsMine)
                    {
                        GetComponent<SkinnedMeshRenderer>().material = userInfoManager.costumeColor.colorMaterial;
                        isDone = true;
                    }
                    else if(playerPhoton.receivedMaterial != -1 && playerPhoton.receivedMaterial != 0)
                    {
                        GetComponent<SkinnedMeshRenderer>().material = FindMaterial(playerPhoton.receivedMaterial);
                        isDone = true;
                    }
                }
            }
            else
            {
                if (userInfoManager != null)
                {
                    GetComponent<SkinnedMeshRenderer>().material = userInfoManager.costumeColor.colorMaterial;
                    isDone = true;
                }
            }
        }
    }

    Material FindMaterial(int colorID)
    {
        foreach(var color in colorStatusList)
        {
            if(playerPhoton.receivedMaterial == color.colorId)
            {
                return color.colorMaterial;
            }
        }

        return colorStatusList[0].colorMaterial;
    }
}
