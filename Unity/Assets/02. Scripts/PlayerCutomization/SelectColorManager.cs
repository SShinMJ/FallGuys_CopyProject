using UnityEngine;

public class SelectColorManager : MonoBehaviour
{
    public GameObject player;
    public Material material;
    public int colorID;

    public void ChangePlayerColor()
    {
        if (!transform.GetChild(0).gameObject.activeSelf)
        {
            player.GetComponent<Renderer>().material = material;
            transform.GetChild(1).gameObject.SetActive(true);
            player.GetComponent<PlayerCurrentCostume>().currentMaterialID = colorID;
            player.GetComponent<PlayerCurrentCostume>().currentMaterialSlot.transform.GetChild(1).gameObject.SetActive(false);
            player.GetComponent<PlayerCurrentCostume>().currentMaterialSlot = gameObject;


            // 변경된 색상값을 서버로 보내줘야함!
            // 바뀔때마다 서버로 보낼 지, 커스텀 창을 나갈때 한번만 보낼 지 결정
            // 후자가 가능한 이유는 어차피 설정창을 나갔을 때 정보만 잘 기억해 두면 되기 때문!
        }
    }
}
