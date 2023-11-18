using Photon.Pun;
using UnityEngine;

public class PlayerCurrentCostume : MonoBehaviour, IPunObservable
{
    // Photon
    public PhotonView pw;

    UserInfoManager userInfoManager;

    public int currentMaterialID;
    public GameObject currentMaterialSlot;
    Material currentMaterial;

    bool isDone = false;

    private void Start()
    {
        userInfoManager = FindObjectOfType<UserInfoManager>();
    }

    private void Update()
    {
        if (gameObject.activeSelf && !isDone)
        {
            if (pw != null)
            {
                if (userInfoManager != null)
                {
                    if (pw.IsMine)
                    {
                        currentMaterial = userInfoManager.costumeColor.colorMaterial;
                        GetComponent<SkinnedMeshRenderer>().material = currentMaterial;
                        isDone = true;
                    }
                    else if(receiveMaterial != null)
                    {
                        GetComponent<SkinnedMeshRenderer>().material = receiveMaterial;
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

    Material receiveMaterial;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentMaterial);
        }
        else
        {
            receiveMaterial = (Material)stream.ReceiveNext();
        }
    }
}
