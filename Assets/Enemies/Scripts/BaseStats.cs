using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : Stats {
    public delegate void ObjectHit(GameObject target, AttackType attack);
    public static event ObjectHit Hit;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
