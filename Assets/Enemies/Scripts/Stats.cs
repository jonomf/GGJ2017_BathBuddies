﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stats : MonoBehaviour {
    public int health;
    public float moveSpeed;
    protected float cooldown;
    public AttackType attackType;
    public DamageController damageController;
    public RangeVolume rangeVolume;
    public int curHealth;
    public string enemyProjectileTag;
    public float attackDelayTime = .1f;
	public Action<Stats> OnDie;
    public GameObject fireTransform = null;
    public Animator animControl = null;
   
    // Use this for initialization
    protected virtual void Start ()
    {
        curHealth = health;
   
        damageController = FindObjectOfType<DamageController>();
    }
	
	// Update is called once per frame
	protected virtual void Update () {

        GameObject target = GetTarget();
        if (target && cooldown <= 0)
        {
 
            cooldown = Attack(target, attackType);
        }
        if(cooldown >= 0)
        {
            //Debug.Log("Cooldown");
            if (cooldown <= Time.deltaTime)
            {
                cooldown = 0;
            }else
            {
                cooldown -= Time.deltaTime;
            }
            
        }
        if(curHealth <= 0)
        {
            Die();
        }
	}
    public GameObject GetTarget()
    {
        if(rangeVolume != null && rangeVolume.activeTargets.Count > 0)
        {
            foreach(GameObject target in rangeVolume.activeTargets)
            {
                return target;
            }
        }
        return null;
    }
    virtual public float Attack(GameObject target, AttackType attack)
    {
        if (animControl)
        {
            animControl.SetTrigger("Fire");
        }
        if (fireTransform)
        {          
            StartCoroutine(DoAttack(attackDelayTime, fireTransform, target, attack));
            return attack.attackCooldown + attackDelayTime;
        }
        else
        {

            return damageController.DoAttack(gameObject, target, attack);
        }
    }
    public IEnumerator DoAttack(float time, GameObject gameObject, GameObject target, AttackType attack) {
        yield return new WaitForSeconds(time);
        damageController.DoAttack(gameObject, target, attack);
    }   
        

    public virtual void Die()
    {
        Debug.Log("I'm DEAADDD");
	    if(OnDie != null)
	    {
		    OnDie(this);
	    }
        Destroy(gameObject);
    }

  
}
