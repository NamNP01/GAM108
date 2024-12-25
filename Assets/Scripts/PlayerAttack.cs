using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Thành phần Animator để xử lý các hoạt ảnh
    public Animator anim;

    // Các thuộc tính tấn công
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    // Prefab cho đạn và điểm xuất phát của đạn
    public GameObject boomerangPrefab;
    public Transform boomerangSpawnPoint;

    // Hướng và tốc độ đạn
    private Vector2 fireDirection;
    public float boomerangSpeed = 10f;
    private GameObject activeBoomerang;
    // Thời gian giữa các lần tấn công
    private float attackTime;
    public float attackTimeDuration = 0f;

    // Khởi động khi bắt đầu
    void Start()
    {
        // Không có thiết lập cụ thể trong Start
    }

    // Cập nhật mỗi khung hình
    void Update()
    {
        // Giảm dần thời gian giữa các lần tấn công
        if (attackTime > 0)
        {
            attackTime -= Time.deltaTime;
        }

        // Xác định hướng bắn khi nhấn phím A hoặc D
        if (Input.GetKeyDown(KeyCode.A))
        {
            fireDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            fireDirection = Vector2.right;
        }

        // Thực hiện tấn công khi nhấn phím J và đủ thời gian giữa các lần tấn công
        if (Input.GetKeyDown(KeyCode.J) && attackTime <= 0)
        {
            Attack();
            attackTime = attackTimeDuration;
        }
    }

    // Hàm tấn công
    void Attack()
    {
        // Kích hoạt hoạt ảnh tấn công
        anim.SetTrigger("Attack");

        if (activeBoomerang == null)
        {
            activeBoomerang = Instantiate(boomerangPrefab, boomerangSpawnPoint.position, Quaternion.identity);
            Rigidbody2D rb = activeBoomerang.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(boomerangSpeed, 0);
        }
    }
    
}
