using UnityEngine;

public class AudioManager : MonoBehaviour
{
	private static AudioManager s_Instance;

	[SerializeField] private AudioSource m_OutOfAmmo;
	[SerializeField] private AudioSource m_PistolShot;

	public static AudioSource outOfAmmo { get { return s_Instance.m_OutOfAmmo; } }
	public static AudioSource pistolShot { get { return s_Instance.m_PistolShot; } }

	void Awake()
	{
		s_Instance = this;
	}
}
