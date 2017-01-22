using System.Collections;
using UnityEngine;

public class CannonHandle : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private ProjectileWeapon m_CannonShot;
	[SerializeField] private ProjectileWeapon m_DepthChargeShot;
	[SerializeField] private float m_RotationSpeed = 100f;
	[SerializeField] private int m_StartingCannonAmmo = 100;
	[SerializeField] private int m_StartingDepthCargeAmmo = 100;
    [Header("References")]
    [SerializeField] private Transform m_YPivot;
    [SerializeField] private Transform m_XPivot;
    [SerializeField] private Transform m_CannonTip;

	private int m_CannonAmmo;
	private int m_DepthCargeAmmo;

	void Awake()
	{
		m_CannonAmmo = m_StartingCannonAmmo;
		m_DepthCargeAmmo = m_StartingDepthCargeAmmo;
	}

    void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(DragHandle());
        }
    }

    IEnumerator DragHandle()
    {
        var lastHandPosition = VRPlayer.rightHand.localPosition;
        VRPlayer.handsBusy = true;
        while (!Input.GetButtonUp("Fire1"))
        {
            m_YPivot.Rotate(Vector3.up, (lastHandPosition - VRPlayer.rightHand.localPosition).x * m_RotationSpeed);
            m_XPivot.Rotate(Vector3.left, (lastHandPosition - VRPlayer.rightHand.localPosition).y * m_RotationSpeed);
            lastHandPosition = VRPlayer.rightHand.localPosition;
            if (VRPlayer.fire)
            {
				AttemptFire();
            }
            yield return null;
        }
        VRPlayer.handsBusy = false;
    }

	void AttemptFire()
	{
		switch (HUDController.cannonMode)
		{
			case HUDController.CannonMode.Ballistic:
				if (m_CannonAmmo > 0)
				{
					m_CannonAmmo--;
					m_CannonShot.Fire(m_CannonTip.position, m_CannonTip.forward);
				}
				else
				{
					AudioManager.outOfAmmo.Play();
				}
				break;
			case HUDController.CannonMode.Depth:
				if (m_StartingDepthCargeAmmo > 0)
				{
					m_StartingDepthCargeAmmo--;
					m_DepthChargeShot.Fire(m_CannonTip.position, m_CannonTip.forward);
				}
				else
				{
					AudioManager.outOfAmmo.Play();
				}
				break;
		}
	}
}
