using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapons/DiscLauncher")]
public class DiscLauncher : ProjectileWeapon
{
    [Header("Settings")]
    [SerializeField] private float m_Power = 1000;
    [Header("References")]
    [SerializeField] private Rigidbody m_Disk;

    public override void Fire(Vector3 position, Vector3 direction)
    {
        var rb = Instantiate(m_Disk.gameObject, position, Quaternion.LookRotation(direction)).GetComponent<Rigidbody>();
        rb.AddForce(rb.transform.forward * m_Power);
    }
}
