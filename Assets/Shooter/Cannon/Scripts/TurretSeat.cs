using System.Collections;
using UnityEngine;

public class TurretSeat : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private ProjectileWeapon m_CannonShot;
    [SerializeField]
    private ProjectileWeapon m_DepthChargeShot;
    [SerializeField]
    private float m_RotationSpeed = 100f;
    [SerializeField]
    private int m_StartingCannonAmmo = 100;
    [SerializeField]
    private int m_StartingDepthCargeAmmo = 100;
    [Header("References")]
    [SerializeField]
    private Transform m_YPivot;
    [SerializeField]
    private Transform m_XPivot;
    [SerializeField]
    private Transform m_CannonTip;

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

            //m_XPivot.rotation = VRPlayer.rightHand.rotation;

            m_XPivot.rotation = Quaternion.RotateTowards(m_XPivot.rotation, Quaternion.LookRotation(m_XPivot.position - VRPlayer.rightHand.position),
                500 * Time.deltaTime);

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
                    //m_CannonAmmo--;
                    m_CannonShot.Fire(m_CannonTip.position, m_CannonTip.forward);
                    AudioManager.Play(SOUNDS.CANNON_SHOT);
                }
                else
                {
                    AudioManager.Play(SOUNDS.OUT_OF_AMMO);
                }
                break;
            case HUDController.CannonMode.Depth:
                if (m_StartingDepthCargeAmmo > 0)
                {
                    //m_StartingDepthCargeAmmo--;
                    m_DepthChargeShot.Fire(m_CannonTip.position, m_CannonTip.forward);
                    AudioManager.Play(SOUNDS.DEPTH_CHARGE_DROP);

                }
                else
                {
                    AudioManager.Play(SOUNDS.OUT_OF_AMMO);
                }
                break;
        }
    }

    /*	float lastFire = 0;
        void Update()
        {
            if(Time.time > lastFire+1)
            {
                AttemptFire();
                lastFire = Time.time;
            }
        }*/
}
