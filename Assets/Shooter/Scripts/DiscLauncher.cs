using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapons/DiscLauncher")]
public class DiscLauncher : ProjectileWeapon {
    public override void Fire()
    {
        Debug.Log("boom!");
    }
}
