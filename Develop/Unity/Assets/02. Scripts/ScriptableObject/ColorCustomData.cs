using UnityEngine;

[CreateAssetMenu(fileName = "CustomColorData_", menuName = "Fallguys/Custom/Color")]
public class ColorCustomData : ScriptableObject
{
    public int colorId;
    public int price;
    public Sprite colorImage;
    public Material colorMaterial;
}
