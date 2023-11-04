using UnityEngine;

// ���̾� ���� �����ϱ� ���� �ʿ�.
[CreateAssetMenu(fileName = "new StateLayerMaskData", menuName = "RPG3D/Animator/StateLayerMaskData")]
public class StateLayerMaskData : ScriptableObject
{
    public UDictionary<State, AnimatorLayers> animatorLayerPairs;
}