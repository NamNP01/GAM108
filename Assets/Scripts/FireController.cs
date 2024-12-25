using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public GameObject Effect;
    private Transform player;
    public GameObject EffTouchEnemy;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }
        GameObject newBullet = Instantiate(Effect, transform.position, Quaternion.identity);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            EnemyHeal enemyHealth = collision.gameObject.GetComponent<EnemyHeal>();
            enemyHealth.TakeDamage(1);
            Destroy(gameObject);
            GameObject newEff = Instantiate(EffTouchEnemy, transform.position, Quaternion.identity);
        }
        if (collision.gameObject.CompareTag("LockKey"))
        {
            // Lấy thành phần LockMap từ đối tượng của người chơi hoặc một đối tượng quản lý chung
            LockMap lockMap = GameObject.FindObjectOfType<LockMap>();
            if (lockMap != null)
            {
                lockMap.CollectLockKey(); // Tăng số lượng lockKey đã thu thập
            }

            Destroy(collision.gameObject); // Hủy đối tượng lockKey
            Destroy(gameObject); // Hủy đối tượng viên đạn
        }
    }
}

