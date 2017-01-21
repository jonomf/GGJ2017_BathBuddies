using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stats : MonoBehaviour {
    public int health;
    public float moveSpeed;
    protected float cooldown;
    public AttackType attackType;
    public DamageController damageController;
    // Use this for initialization
    void Start ()
    {
        damageController = FindObjectOfType<DamageController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Attack()
    {
        
    }
    public virtual void Die()
    {
        Debug.Log("I'm DEAADDD");
        Destroy(gameObject);
    }
  
}
