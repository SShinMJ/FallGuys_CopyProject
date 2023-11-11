using UnityEngine;

// ���̾� ���� �����ϱ� ���� �ʿ�.
[CreateAssetMenu(fileName = "new StateLayerMaskData", menuName = "Fallguys/Animator/StateLayerMaskData")]
public class StateLayerMaskData : ScriptableObject
{
    public UDictionary<State, AnimatorLayers> animatorLayerPairs;
}