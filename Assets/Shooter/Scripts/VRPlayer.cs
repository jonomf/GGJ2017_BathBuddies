using UnityEngine;
using UnityEngine.VR;

public class VRPlayer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private ProjectileWeapon weapon;
    [Header("References")]
    [SerializeField] private Transform m_LeftHand;
    [SerializeField] private Transform m_RightHand;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            weapon.Fire();
        }
        m_LeftHand.localPosition = InputTracking.GetLocalPosition(VRNode.LeftHand);
        m_RightHand.localPosition = InputTracking.GetLocalPosition(VRNode.RightHand);
    }
}
