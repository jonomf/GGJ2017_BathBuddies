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
        if (Input.GetButton("Fire1") && !VRPlayer.handsBusy)
        {
            StartCoroutine(DragHandle());
        }
    }

    IEnumerator DragHandle()
    {
	    var lastHandPosition = HandPositionRelativeToHead();
        VRPlayer.handsBusy = true;
	    var handStartRotation = VRPlayer.rightHand.rotation;
        while (!Input.GetButtonUp("Fire1"))
		{
			
			//m_YPivot.Rotate(m_YPivot.up, (lastHandPosition - HandPositionRelativeToHead()).x * m_RotationSpeed * 50 * Time.deltaTime, Space.World);
			//m_XPivot.Rotate(-m_XPivot.right, (lastHandPosition - HandPositionRelativeToHead()).y * m_RotationSpeed * 50 * Time.deltaTime, Space.World);
			//lastHandPosition = HandPositionRelativeToHead();

			//m_XPivot.rotation = handStartRotation * Quaternion.Inverse(VRPlayer.rightHand.rotation);

			m_XPivot.rotation = VRPlayer.rightHand.rotation;

            if (VRPlayer.fire)
            {
				AttemptFire();
            }
            yield return null;
        }
        VRPlayer.handsBusy = false;
	}

	Vector3 HandPositionRelativeToHead()
	{
		return VRPlayer.head.InverseTransformPoint(VRPlayer.rightHand.position);
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
					AudioManager.Play(SOUNDS.OUT_OF_AMMO);
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
					AudioManager.Play(SOUNDS.OUT_OF_AMMO);
				}
				break;
		}
	}
}
