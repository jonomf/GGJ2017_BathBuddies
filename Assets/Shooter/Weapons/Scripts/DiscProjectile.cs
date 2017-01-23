using System.Collections;
using UnityEngine;

public class DiscProjectile : MonoBehaviour
{

    [SerializeField] private float m_DestroyDelay = 1f;

	public GameObject sonarView;

    void OnCollisionEnter(Collision collision)
    {
        //StartCoroutine(DestroyAfterDelay());
		Destroy(gameObject);
    }


    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(m_DestroyDelay);
        Destroy(gameObject);
    }

	void Update()
	{
		if(this.transform.position.y < -10)
		{
			Destroy(this.gameObject);
		}
	}

	void OnDestroy()
	{
		if(this.transform.position.y < 0 && this.sonarView != null)
		{
			GameObject.Instantiate(this.sonarView, this.transform.position, Quaternion.identity);
		}
	}

}
