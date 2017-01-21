using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {
    public delegate void ClickAction(GameObject target, AttackType attack);
    public event ClickAction OnClicked;
    // Use this for initialization
    void Start ()
    {
        OnClicked += TakeDamage;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DoAttack()
    {
        
    }

    public void TakeDamage(GameObject target, AttackType attack)
    {
        
    }
}
