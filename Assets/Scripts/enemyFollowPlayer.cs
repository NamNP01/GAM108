using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    [Header("Cài đặt di chuyển")]
    public float speed; // Tốc độ di chuyển của kẻ địch
    public float lineOfSight; // Phạm vi nhìn thấy người chơi

    [Header("Cài đặt bắn")]
    public float shootingRange; // Phạm vi bắn
    public float fireRate; // Tốc độ bắn
    private float nextFireTime; // Thời gian tiếp theo có thể bắn

    public GameObject bullet; // Đạn
    public GameObject bulletParent; // Vị trí xuất hiện đạn
    private Transform player; // Người chơi
    public Transform basePoint; // Điểm cơ sở để quay về
    public Animator anim;


    void Start()
    {
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Tìm người chơi theo tag

        // Tính khoảng cách từ kẻ địch đến người chơi
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        // Nếu khoảng cách trong phạm vi nhìn thấy nhưng ngoài phạm vi bắn
        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > shootingRange)
        {
            // Di chuyển về phía người chơi
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);

            // Điều chỉnh hướng của kẻ địch về phía người chơi
            if (transform.position.x < player.position.x)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }
        }
        // Nếu khoảng cách trong phạm vi bắn và có thể bắn
        else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        {
            anim.SetTrigger("Attack");
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity); // Tạo đạn
            nextFireTime = Time.time + fireRate; // Cập nhật thời gian tiếp theo có thể bắn
        }
        // Nếu người chơi ngoài phạm vi bắn
        else if (distanceFromPlayer > lineOfSight)
        {
            // Di chuyển về điểm cơ sở
            transform.position = Vector2.MoveTowards(this.transform.position, basePoint.position, speed * Time.deltaTime);

            // Điều chỉnh hướng của kẻ địch về phía điểm cơ sở
            if (transform.position.x < basePoint.position.x)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }
        }
    }

    // Vẽ phạm vi nhìn thấy và phạm vi bắn trong chế độ Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
