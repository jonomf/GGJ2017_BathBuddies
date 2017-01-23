using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    public delegate void ObjectHit(GameObject target, AttackType attack);
    public static event ObjectHit Hit;
   

    // Use this for initialization
	protected override void Start () {
        base.Start();
	}

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    public override float Attack(GameObject target, AttackType attack)
    {
        return base.Attack(target, attack);
    }

	void OnTriggerEnter(Collider other)
	{
		var proj = other.GetComponent<Projectile>();
		if (proj == null)
			return;
		if (proj.attack.originator != "player_depth")
			return;
		ApplyDamage(proj.attack, other.gameObject);
	}

	void ApplyDamage(AttackType attack, GameObject hit)
	{
		if (attack != null && attack.originator == enemyProjectileTag)
		{
			Hit(gameObject, attack);
			DestroyObject(hit);
			if (attack.hitEffect != null)
			{
				GameObject effect = Instantiate(attack.hitEffect);
				effect.transform.position = hit.transform.position;
				Destroy(effect, 2.0f);
			}
		}

	}
    private void OnCollisionEnter(Collision collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        AttackType attack = null;
        if (projectile != null)
        {
            attack = projectile.attack;
        }
		ApplyDamage(attack, collision.gameObject);
    }
}
