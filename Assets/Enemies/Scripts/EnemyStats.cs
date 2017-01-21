using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    public delegate void ObjectHit(GameObject target, AttackType attack);
    public static event ObjectHit Hit;
   

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision enter)
    {
        if(enter.gameObject.tag == "Player")
        {
            AttackType attack = enter.gameObject.GetComponent<AttackType>();
            Hit(gameObject,attack);
        }
    }
}
