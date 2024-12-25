using System.Collections;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform bulletSpawnPoint; 
    public float bulletSpeed = 10f; 
    public float fireInterval = 5f; 

    public bool isShootingRight = true; 

    private void Start()
    {
        StartCoroutine(FireBullet());
    }

    private IEnumerator FireBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireInterval); // Đợi fireInterval giây trước khi bắn

            // Sinh viên đạn từ prefab tại điểm spawn
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

            // Lấy thành phần Rigidbody2D của viên đạn và thiết lập vận tốc dựa trên hướng của cái bẫy
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Xác định hướng bắn dựa trên biến isShootingRight
            if (isShootingRight)
            {
                rb.velocity = transform.right * bulletSpeed; // Bắn sang phải
            }
            else
            {
                rb.velocity = -transform.right * bulletSpeed; // Bắn sang trái
            }
        }
    }
}
