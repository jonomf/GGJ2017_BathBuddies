using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stats : MonoBehaviour {
    public int health;
    public float moveSpeed;
    public GameObject[] TargetsInRange = null;
    protected float cooldown;
    public AttackType attackType;
    public DamageController damageController;
    // Use this for initialization
    void Start ()
    {
        //damageController = FindObjectOfType<DamageController>();
    }
	
	// Update is called once per frame
	void Update () {
		if(TargetsInRange.Length >= 1)
        {
            Attack(TargetsInRange[0], attackType);
        }
	}
    public GameObject[] GetTargetsInRange()
    {

    }
    public void Attack(GameObject target, AttackType attack)
    {
        damageController.DoAttack(gameObject, target, attack);
    }
    public virtual void Die()
    {
        Debug.Log("I'm DEAADDD");
        Destroy(gameObject);
    }
  
}
