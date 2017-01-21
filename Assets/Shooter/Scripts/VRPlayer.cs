using UnityEngine;
using UnityEngine.VR;

public class VRPlayer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private ProjectileWeapon m_ActiveWeapon;
    [Header("References")]
    [SerializeField] private Transform m_LeftHand;
    [SerializeField] private Transform m_RightHand;

    void Update()
    {
        var leftHandPosition = InputTracking.GetLocalPosition(VRNode.LeftHand);
        var leftHandForward = InputTracking.GetLocalRotation(VRNode.LeftHand);
        var rightHandPosition = InputTracking.GetLocalPosition(VRNode.RightHand);
        var rightHandForward = InputTracking.GetLocalRotation(VRNode.RightHand);
        m_LeftHand.localPosition = leftHandPosition;
        m_LeftHand.localRotation = leftHandForward;
        m_RightHand.localPosition = rightHandPosition;
        m_RightHand.localRotation = rightHandForward;
        if (Input.GetButtonDown("Fire1"))
        {
            m_ActiveWeapon.Fire(m_RightHand.position, m_RightHand.forward);
        }
    }
}
