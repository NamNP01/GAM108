using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float gravity = 9.8f; // Gia tốc trọng lực
    private Vector2 velocity;
    public GameObject EffTouchEnemy;


    public void Shoot(Vector2 initialVelocity)
    {
        velocity = initialVelocity;
    }

    void Update()
    {
        // Áp dụng trọng lực vào vận tốc theo trục y
        velocity.y -= gravity * Time.deltaTime;

        // Cập nhật vị trí của mũi tên
        transform.position += (Vector3)velocity * Time.deltaTime;

        // Cập nhật góc quay của mũi tên dựa trên vận tốc
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            GameObject newEff = Instantiate(EffTouchEnemy, transform.position, Quaternion.identity);

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
