using System.Collections;
using UnityEngine;

public class CannonHandle : MonoBehaviour
{
    [SerializeField] private Transform m_YPivot;
    [SerializeField] private Transform m_XPivot;

    public float m_RotationSpeed = 10f;

    void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(DragHandle());
        }
    }

    IEnumerator DragHandle()
    {
        var lastHandPosition = VRPlayer.rightHand.position;
        while (!Input.GetButtonUp("Fire1"))
        {
            m_YPivot.Rotate(Vector3.up, (lastHandPosition - VRPlayer.rightHand.position).x * m_RotationSpeed);
            m_XPivot.Rotate(Vector3.left, (lastHandPosition - VRPlayer.rightHand.position).y * m_RotationSpeed);
            lastHandPosition = VRPlayer.rightHand.position;
            yield return null;
        }
    }
}
