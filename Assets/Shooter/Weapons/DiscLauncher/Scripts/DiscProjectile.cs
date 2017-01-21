using System.Collections;
using UnityEngine;

public class DiscProjectile : MonoBehaviour
{

    [SerializeField] private float m_DestroyDelay = 1f;

    void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(m_DestroyDelay);
        Destroy(gameObject);
    }
}
