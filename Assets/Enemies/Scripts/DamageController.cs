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
	void Update () {
		
	}

    public void DoAttack(GameObject attacker, GameObject target, AttackType attack)
    {
        
    }

    public void TakeDamage(GameObject target, AttackType attack)
    {
        Debug.Log("Target is taking damage!");
        
    }
}
