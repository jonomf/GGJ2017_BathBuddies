using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

    // Use this for initialization
    void Start ()
    {
        EnemyStats.Hit += TakeDamage;
        BaseStats.Hit += TakeDamage;
    }
	
	// Update is called once per frame
	
    public float DoAttack(GameObject attacker, GameObject target, AttackType attack)
    {
        GameObject projectile = Instantiate(attack.projectile);
        projectile.transform.position = attacker.transform.position;
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        Vector3 directionalVector = (target.transform.position - projectile.transform.position).normalized;
        rb.AddForce(directionalVector * attack.attackForce);
        projectile.GetComponent<Projectile>().attack = attack;
        return attack.attackCooldown;
    }

    public void TakeDamage(GameObject target, AttackType attack)
    {

        Stats stats = target.GetComponent<Stats>();
        stats.curHealth -= attack.damage;
    }
}
