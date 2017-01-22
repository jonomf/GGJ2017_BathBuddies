using System.Collections;
using UnityEngine;
using UnityEngine.VR;

public class VRPlayer : MonoBehaviourSingleton<VRPlayer>
{
    public static Transform leftHand { get { return Instance.m_LeftHand; } }
	public static Transform rightHand { get { return Instance.m_RightHand; } }
	public static Transform head { get { return Instance.m_Head; } }
    public static bool fire { get; private set; }
    public static bool handsBusy { get; set; }

    [Header("Settings")]
    [SerializeField] private ProjectileWeapon m_ActiveWeapon;
    [SerializeField] private float m_TeleportFadeDuration = 0.5f;
	[SerializeField] private bool m_PistolEnabled;
    [Header("Input business")]
    [SerializeField] private float m_TriggerOnThreshhold = 0.6f;
    [SerializeField] private float m_TriggerOffThreshhold = 0.4f;
    [Header("References")]
    [SerializeField] private Transform m_LeftHand;
    [SerializeField] private Transform m_RightHand;
	[SerializeField] private Transform m_Head;
    [SerializeField] private Material m_FadeMaterial;

    private readonly Color m_Opaque = new Color(0, 0, 0, 1);
    private readonly Color m_Invisible = new Color(0, 0, 0, 0);
    private bool m_TriggerConsumed;

    protected override void Awake()
    {
        m_FadeMaterial.color = new Color(0,0,0,0);
    }

    void Update()
    {
        m_LeftHand.localPosition = InputTracking.GetLocalPosition(VRNode.LeftHand);
        m_LeftHand.localRotation = InputTracking.GetLocalRotation(VRNode.LeftHand);
        m_RightHand.localPosition = InputTracking.GetLocalPosition(VRNode.RightHand);
        m_RightHand.localRotation = InputTracking.GetLocalRotation(VRNode.RightHand);
        if (m_PistolEnabled && !handsBusy && fire)
        {
            m_ActiveWeapon.Fire(m_RightHand.position, m_RightHand.forward);
			AudioManager.Play(SOUNDS.PISTOL_SHOT);
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

    public static void TeleportTo(Transform target, System.Action onComplete = null)
    {
		Instance.StartCoroutine(Instance.TeleportWithFade(target.position, target.forward, onComplete));
    }

    private static Color FadeToBlack(float alpha)
    {
        return new Color(0, 0, 0, alpha);
    }

	IEnumerator TeleportWithFade(Vector3 targetPosition, Vector3 direction,  System.Action onComplete)
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

		if(onComplete != null)
		{
			onComplete();
		}
    }
}
