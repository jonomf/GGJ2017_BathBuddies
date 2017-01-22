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
		projectile.transform.rotation = Quaternion.LookRotation(directionalVector); // set projectile to look at the target.
        rb.AddForce(directionalVector * attack.attackForce);
        projectile.GetComponent<Projectile>().attack = attack;
   
		// set target for any seeking behaviors.
		var projectileScript = projectile.GetComponent<Projectile>();
		projectileScript.target = target;

        projectile.transform.parent = MainGame.enemyBulletsContainer;

        if(attack.fireEffect != null)
        {
            GameObject effect = Instantiate(attack.fireEffect);
            //effect.transform.LookAt()
            effect.transform.position = attacker.transform.position;
            Destroy(effect, 2.0f);
        }
        if(attack.attackSound != SOUNDS.NO_SOUND)
        {
            AudioManager.Play(attack.attackSound, attacker.transform.position);
        }
        return attack.attackCooldown;
    }

    public void TakeDamage(GameObject target, AttackType attack)
    {

        Stats stats = target.GetComponent<Stats>();
        stats.curHealth -= attack.damage;
    }
}
