using UnityEngine;

public class PrefabNester : MonoBehaviour
{
	[SerializeField] private bool m_Active;
	[SerializeField] private GameObject m_Prefab;

	void Start()
	{
		if (!m_Active)
			return;
		var instance = Instantiate(m_Prefab, transform).transform;
		instance.localPosition = Vector3.zero;
		instance.localRotation = Quaternion.identity;
	}
}
