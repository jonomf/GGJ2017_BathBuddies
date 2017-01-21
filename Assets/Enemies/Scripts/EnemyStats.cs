using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    public delegate void ObjectHit(GameObject target, AttackType attack);
    public static event ObjectHit Hit;
   

    // Use this for initialization
    void Start () {
        base.Start();
	}

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }


    private void OnCollisionEnter(Collision collision)
    {
        AttackType attack = collision.gameObject.GetComponent<AttackType>();
        if (attack != null && attack.originator == enemyProjectileTag)
        {
            Hit(gameObject, attack);
            DestroyObject(collision.gameObject);
        }
    }
}
