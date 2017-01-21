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
        m_LeftHand.localPosition = InputTracking.GetLocalPosition(VRNode.LeftHand);
        m_LeftHand.localRotation = InputTracking.GetLocalRotation(VRNode.LeftHand);
        m_RightHand.localPosition = InputTracking.GetLocalPosition(VRNode.RightHand);
        m_RightHand.localRotation = InputTracking.GetLocalRotation(VRNode.RightHand);
        if (Input.GetButtonDown("Fire1"))
        {
            m_ActiveWeapon.Fire(m_RightHand.position, m_RightHand.forward);
        }
    }
}
