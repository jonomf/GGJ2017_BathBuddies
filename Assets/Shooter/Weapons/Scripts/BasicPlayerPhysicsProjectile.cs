using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapons/BasicPlayerPhysicsProjectile")]
public class BasicPlayerPhysicsProjectile : ProjectileWeapon
{
    [Header("Settings")]
    [SerializeField] private float m_Power = 1000;
    [Header("References")]
    [SerializeField] private Rigidbody m_Projectile;

    public override void Fire(Vector3 position, Vector3 direction)
    {
        var rb = Instantiate(m_Projectile.gameObject, position, Quaternion.LookRotation(direction)).GetComponent<Rigidbody>();
        rb.AddForce(direction * m_Power);
	    rb.transform.parent = MainGame.playerBulletsContainer;
    }
}
