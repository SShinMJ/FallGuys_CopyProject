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


            // ����� ������ ������ ���������!
            // �ٲ𶧸��� ������ ���� ��, Ŀ���� â�� ������ �ѹ��� ���� �� ����
            // ���ڰ� ������ ������ ������ ����â�� ������ �� ������ �� ����� �θ� �Ǳ� ����!
        }
    }
}
