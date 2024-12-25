using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public float gravity = 9.8f; // Gia tốc trọng lực
    public float rotationSpeed = 360f; // Tốc độ quay 360 độ mỗi giây
    public float returnSpeed = 10f; // Tốc độ quay trở về của boomerang
    public GameObject EffTouchEnemy; // Hiệu ứng khi chạm vào mục tiêu
    public float maxDistance = 20f; // Khoảng cách tối đa từ người chơi mà boomerang có thể đi


    private Vector2 velocity; // Vận tốc của boomerang
    private bool returning = false; // Biến đánh dấu boomerang đang quay về
    private bool bounced = false; // Biến đánh dấu boomerang đã nảy lần đầu tiên
    private Transform player; // Đối tượng mà boomerang sẽ quay về (thường là người chơi)

    void Start()
    {
        // Đặt đối tượng ban đầu là người chơi
        player = GameObject.FindGameObjectWithTag("Player").transform; // Tìm người chơi theo tag
    }

    void Update()
    {
        if (!returning)
        {
            // Kiểm tra khoảng cách giữa boomerang và người chơi
            if (Vector2.Distance(transform.position, player.position) > maxDistance)
            {
                returning = true;
            }

            // Áp dụng trọng lực vào vận tốc theo trục y
            velocity.y -= gravity * Time.deltaTime;

            // Cập nhật vị trí của boomerang
            transform.position += (Vector3)velocity * Time.deltaTime;

            // Cập nhật góc quay của boomerang dựa trên vận tốc
            float angle = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, angle);
        }
        else
        {
            // Quay về người chơi
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, returnSpeed * Time.deltaTime);
        }
    }

    // Phương thức này được gọi khi bắn boomerang
    public void Shoot(Vector2 initialVelocity)
    {
        velocity = initialVelocity;
    }

    // Xử lý khi boomerang va chạm với các collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!bounced)
            {
                // Nảy ra hướng ngẫu nhiên
                Instantiate(EffTouchEnemy, transform.position, Quaternion.identity);
                velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * velocity.magnitude;
                bounced = true;
            }
            else
            {
                // Quay về người chơi
                returning = true;
            }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            EnemyHeal enemyHealth = collision.gameObject.GetComponent<EnemyHeal>();
            enemyHealth.TakeDamage(1);
            if (!bounced)
            {
                // Nảy ra hướng ngẫu nhiên
                Instantiate(EffTouchEnemy, transform.position, Quaternion.identity);
                velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * velocity.magnitude;
                bounced = true;
            }
            else
            {
                // Quay về người chơi
                returning = true;
            }
        }
        if (collision.gameObject.CompareTag("Player") && returning)
        {
            DestroyBoomerang();
        }
        if (collision.gameObject.CompareTag("LockKey"))
        {
            // Lấy thành phần LockMap từ đối tượng của người chơi hoặc một đối tượng quản lý chung
            LockMap lockMap = GameObject.FindObjectOfType<LockMap>();
            if (lockMap != null)
            {
                lockMap.CollectLockKey(); // Tăng số lượng lockKey đã thu thập
            }
            Instantiate(EffTouchEnemy, transform.position, Quaternion.identity);

            Destroy(collision.gameObject); // Hủy đối tượng lockKey
            Destroy(gameObject); // Hủy đối tượng viên đạn
        }
    }

    // Phương thức hủy boomerang, hiệu ứng và hủy đối tượng
    private void DestroyBoomerang()
    {
        Destroy(gameObject); // Hủy đối tượng boomerang
    }
}
