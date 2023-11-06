using TMPro;
using UnityEngine;

public class BlinkText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI waitingText;

    float countTime = 0;
    bool isMax = false;
    Color startColor = new Color32(255, 255, 255, 0);
    Color endColor = new Color32(255, 255, 255, 255);

    private void Update()
    {
        if (waitingText.color.a >= 0.99)
        {
            isMax = true;
            countTime = 0.01f;
        }
        else if (waitingText.color.a <= 0.01)
        {
            isMax = false;
            countTime = 0.03f;
        }

        countTime += Time.deltaTime;

        if (isMax)
        {
            waitingText.color = Color32.Lerp(endColor, startColor, countTime / 2);
        }
        else
            waitingText.color = Color32.Lerp(startColor, endColor, countTime / 2);
    }
}
