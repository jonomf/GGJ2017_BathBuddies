using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : Stats {

    public delegate void ObjectHit(GameObject target, AttackType attack);
    public static event ObjectHit Hit;
    // Use this for initialization
	protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
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
	            if(Hit != null) Hit(gameObject, attack);
	            DestroyObject(collision.gameObject);
                if (attack.hitEffect != null)
                {
                    GameObject effect = Instantiate(attack.hitEffect);
                    effect.transform.position = collision.transform.position;
                    Destroy(effect, 2.0f);
                }
                if(attack.hitSound != SOUNDS.NO_SOUND)
                {
                    AudioManager.Play(attack.hitSound, collision.contacts[0].point);
                }

            }
        }
    }
}
