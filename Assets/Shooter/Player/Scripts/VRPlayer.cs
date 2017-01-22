﻿using System.Collections;
using UnityEngine;
using UnityEngine.VR;

public class VRPlayer : MonoBehaviour
{
    private static VRPlayer s_Instance;

    public static Transform leftHand { get { return s_Instance.m_LeftHand; } }
    public static Transform rightHand { get { return s_Instance.m_RightHand; } }

    [Header("Settings")]
    [SerializeField] private ProjectileWeapon m_ActiveWeapon;
    [SerializeField] private float m_TeleportFadeDuration = 0.5f;
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
        if (Input.GetButtonDown("Fire2"))
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
    }

    public static void TeleportTo(Transform target)
    {
        s_Instance.StartCoroutine(s_Instance.TeleportWithFade(target.position));
    }

    private static Color FadeToBlack(float alpha)
    {
        return new Color(0, 0, 0, alpha);
    }

    IEnumerator TeleportWithFade(Vector3 targetPosition)
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
        while (t > 0)
        {
            m_FadeMaterial.color = FadeToBlack(t);
            t -= Time.unscaledDeltaTime * (1 / m_TeleportFadeDuration);
            yield return null;
        }
        m_FadeMaterial.color = m_Invisible;
    }
}