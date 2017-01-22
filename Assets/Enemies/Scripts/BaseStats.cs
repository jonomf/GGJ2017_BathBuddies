using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : Stats {

    public delegate void ObjectHit(GameObject target, AttackType attack);
    public static event ObjectHit Hit;
    // Use this for initialization
    void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}
    private void OnCollisionEnter(Collision collision)
    {
        var projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            AttackType attack = projectile.attack;
            if (attack != null && attack.originator == enemyProjectileTag)
            {
                Hit(gameObject, attack);
                DestroyObject(collision.gameObject);
            }
        }
    }
}
