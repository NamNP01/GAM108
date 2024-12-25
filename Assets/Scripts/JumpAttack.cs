using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : MonoBehaviour
{
    void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            EnemyHeal enemyHealth = collision.gameObject.GetComponent<EnemyHeal>();
            enemyHealth.TakeDamage(1);
            Destroy(gameObject,2f);
        }
    }
}
