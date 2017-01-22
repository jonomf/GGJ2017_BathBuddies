using System.Collections;
using UnityEngine;
using UnityEngine.VR;

public class VRPlayer : MonoBehaviour
{
    private static VRPlayer s_Instance;

    public static Transform leftHand { get { return s_Instance.m_LeftHand; } }
    public static Transform rightHand { get { return s_Instance.m_RightHand; } }
    public static bool fire { get; private set; }
    public static bool handsBusy { get; set; }

    [Header("Settings")]
    [SerializeField] private ProjectileWeapon m_ActiveWeapon;
    [SerializeField] private float m_TeleportFadeDuration = 0.5f;
    [Header("Input business")]
    [SerializeField] private float m_TriggerOnThreshhold = 0.6f;
    [SerializeField] private float m_TriggerOffThreshhold = 0.4f;
    [Header("References")]
    [SerializeField] private Transform m_LeftHand;
    [SerializeField] private Transform m_RightHand;
    [SerializeField] private Material m_FadeMaterial;

    [Header("Teleport testing")]
    [SerializeField]
    private Transform m_TeleSpot1;
    [SerializeField]
    private Transform m_TeleSpot2;
    [SerializeField]
    private Transform m_TeleSpot3;

    private readonly Color m_Opaque = new Color(0, 0, 0, 1);
    private readonly Color m_Invisible = new Color(0, 0, 0, 0);
    private bool m_TriggerConsumed;

    void Awake()
    {
        s_Instance = this;
        m_FadeMaterial.color = new Color(0,0,0,0);
    }

    void Update()
    {
        m_LeftHand.localPosition = InputTracking.GetLocalPosition(VRNode.LeftHand);
        m_LeftHand.localRotation = InputTracking.GetLocalRotation(VRNode.LeftHand);
        m_RightHand.localPosition = InputTracking.GetLocalPosition(VRNode.RightHand);
        m_RightHand.localRotation = InputTracking.GetLocalRotation(VRNode.RightHand);
        if (!handsBusy && fire)
        {
            m_ActiveWeapon.Fire(m_RightHand.position, m_RightHand.forward);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TeleportTo(m_TeleSpot1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TeleportTo(m_TeleSpot2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TeleportTo(m_TeleSpot3);
        }

        if (fire)
        {
            fire = false;
        }
        if (Input.GetAxis("Trigger") > m_TriggerOnThreshhold && !m_TriggerConsumed)
        {
            m_TriggerConsumed = true;
            fire = true;
        }
        else if (Input.GetAxis("Trigger") < m_TriggerOffThreshhold && m_TriggerConsumed)
        {
            m_TriggerConsumed = false;
        }
    }

    public static void TeleportTo(Transform target)
    {
        s_Instance.StartCoroutine(s_Instance.TeleportWithFade(target.position, target.forward));
    }

    private static Color FadeToBlack(float alpha)
    {
        return new Color(0, 0, 0, alpha);
    }

    IEnumerator TeleportWithFade(Vector3 targetPosition, Vector3 direction)
    {
        var t = 0f;
        while (t < 1)
        {
            m_FadeMaterial.color = FadeToBlack(t);
            t += Time.unscaledDeltaTime * (1 / m_TeleportFadeDuration);
            yield return null;
        }
        m_FadeMaterial.color = m_Opaque;
        transform.position = targetPosition;
		transform.rotation = Quaternion.Euler(direction);
        while (t > 0)
        {
            m_FadeMaterial.color = FadeToBlack(t);
            t -= Time.unscaledDeltaTime * (1 / m_TeleportFadeDuration);
            yield return null;
        }
        m_FadeMaterial.color = m_Invisible;
    }
}
