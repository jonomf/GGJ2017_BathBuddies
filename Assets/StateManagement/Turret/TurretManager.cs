using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
	private static TurretManager s_Instance;

	[SerializeField] private List<Turret> m_Turrets;

	public static Turret startingTurret { get { return s_Instance.m_Turrets[0]; } }

	void Awake()
	{
		s_Instance = this;
	}
}
