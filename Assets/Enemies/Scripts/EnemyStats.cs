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
    private void OnCollisionEnter(Collision collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        AttackType attack = null;
        if (projectile != null)
        {
            attack = projectile.attack;
        }

        if (attack != null && attack.originator == enemyProjectileTag)
        {
            Hit(gameObject, attack);
            DestroyObject(collision.gameObject);
            if(attack.hitEffect != null)
            {
                GameObject effect = Instantiate(attack.hitEffect);
                effect.transform.position = collision.transform.position;
                Destroy(effect, 2.0f);
            }
        }
    }
}
