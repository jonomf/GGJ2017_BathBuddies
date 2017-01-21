using UnityEngine;
using UnityEngine.VR;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform m_LeftHand, m_RightHand;

    void Update()
    {
        m_LeftHand.localPosition = InputTracking.GetLocalPosition(VRNode.LeftHand);
        m_RightHand.localPosition = InputTracking.GetLocalPosition(VRNode.RightHand);
    }
}
