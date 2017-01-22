using UnityEngine;
using System.Collections;



public class MonoBehaviourSingleton<T> : MonoBehaviour where T : class {

    private static T instance;

    public static T Instance { 
        get {
            if (instance == null)
            {
                var r = FindObjectOfType(typeof(T));
                var i = r as T;
                return i;
            }
            else
            {
                return instance;
            }
        }
    }
    // Use this for initialization
    protected virtual void Awake () {
        instance = this as T;
	}

    protected virtual void OnDestroy()
    {
        instance = null;
    }
	
}
