using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeVolume : MonoBehaviour {
    public string targetTag = "Base";
    public HashSet<GameObject> activeTargets = new HashSet<GameObject>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Collision Detected");
        if (other.gameObject.tag == targetTag)
        {
            activeTargets.Add(other.gameObject);
            Debug.Log("Adding enemy to available targets");
        }
        
          
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == targetTag)
        {
            activeTargets.Remove(other.gameObject);
            Debug.Log("Removing enemy from avaialble targets");
            
        }
    }
}
