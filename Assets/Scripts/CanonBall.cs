using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    public GameObject Effect;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameObject newBullet = Instantiate(Effect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameObject newBullet = Instantiate(Effect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
