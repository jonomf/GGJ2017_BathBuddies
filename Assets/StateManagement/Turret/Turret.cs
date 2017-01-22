using UnityEngine;

//NOTE: just a placeholder for when we do have any logic on a turret
public class Turret : MonoBehaviour
{

    [SerializeField] private Transform m_TeleportPoint;
    public Transform teleportPoint { get { return m_TeleportPoint; } }
}
