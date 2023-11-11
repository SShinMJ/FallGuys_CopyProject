using UnityEngine;

// 레이어 별로 관리하기 위해 필요.
[CreateAssetMenu(fileName = "new StateLayerMaskData", menuName = "Fallguys/Animator/StateLayerMaskData")]
public class StateLayerMaskData : ScriptableObject
{
    public UDictionary<State, AnimatorLayers> animatorLayerPairs;
}