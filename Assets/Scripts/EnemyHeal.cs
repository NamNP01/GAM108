using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeal : MonoBehaviour
{
    public float HP;
    public GameObject ScorePrefab;
    public Animator anim;
    private bool isDead = false;
    public GameObject[] itemPrefabs;
    public GameObject Parent;

    

    

    public void TakeDamage(int damage)
    {
        if (HP <= 0)
            return; // Bỏ qua nếu đã chết

        HP -= damage;

        if (HP <= 0)
        {
            Die();
        }
        else
        {
            anim.SetTrigger("Hurt");
        }
    }

    private void Die()
    {
        if (isDead)
            return;

        anim.SetTrigger("Die");
        isDead = true;

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
            collider.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.zero;

        Destroy(Parent, 0.3f);

        if (ScorePrefab != null)
        {
            GameObject newItem = Instantiate(ScorePrefab, transform.position, Quaternion.identity);
        }

        if (itemPrefabs.Length > 0)
        {
            // Chọn ngẫu nhiên một vật phẩm từ danh sách
            int randomIndex = Random.Range(0, itemPrefabs.Length);
            GameObject randomItemPrefab = itemPrefabs[randomIndex];

            if (randomItemPrefab != null)
            {
                GameObject newItem = Instantiate(randomItemPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
