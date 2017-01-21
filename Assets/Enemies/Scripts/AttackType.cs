using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/AttackObject")]
public class AttackType : ScriptableObject
{
    public int damage;
    public int range;
    public string originator;
    public GameObject projectile;
    public GameObject fireEffect;
    public GameObject hitEffect;
    public float attackForce;
    public float attackCooldown;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
