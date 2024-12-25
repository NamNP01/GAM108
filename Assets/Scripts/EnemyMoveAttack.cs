using System.Collections;
using UnityEngine;

public class EnemyMoveDashAI : MonoBehaviour
{
    [Header("Patrol Settings")]
    public Transform pointA; // Điểm tuần tra A
    public Transform pointB; // Điểm tuần tra B
    public float patrolSpeed = 2f; // Tốc độ tuần tra

    [Header("Attack Settings")]
    public float detectionRange = 5f; // Phạm vi phát hiện người chơi
    public float dashSpeed = 10f; // Tốc độ lướt
    public float dashDistance = 3f; // Khoảng cách lướt
    public float dashCooldown = 2f; // Thời gian hồi lướt
    public float dashDelay = 0.5f; // Thời gian chờ trước khi lướt

    private Vector3 targetPoint; // Điểm mục tiêu tuần tra
    private Transform player; // Transform của người chơi
    private Vector3 originalPosition; // Vị trí ban đầu của kẻ địch
    private bool isChasing = false; // Trạng thái tấn công
    private bool isDashing = false; // Trạng thái lướt
    private float nextDashTime; // Thời gian tiếp theo có thể lướt

    public Animator anim; // Thêm biến Animator để điều khiển hoạt hình

    void Start()
    {
        targetPoint = pointA.position; // Bắt đầu tuần tra từ điểm A
        originalPosition = transform.position; // Lưu vị trí ban đầu của kẻ địch
        
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Tìm người chơi theo tag
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true; // Chuyển sang trạng thái tấn công
        }
        else if (distanceToPlayer > detectionRange)
        {
            isChasing = false; // Quay lại tuần tra
        }

        if (isChasing && !isDashing && Time.time >= nextDashTime)
        {
            StartCoroutine(Dash());
        }
        else if (!isChasing && !isDashing)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        anim.SetBool("isWalking", true); // Chuyển sang hoạt hình đi bộ
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, patrolSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            targetPoint = targetPoint == pointA.position ? pointB.position : pointA.position; // Chuyển sang điểm tuần tra ngược lại
        }

        // Điều chỉnh hướng của kẻ địch
        FlipTowards(targetPoint);
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        anim.SetTrigger("Dash");

        // Đợi một khoảng thời gian trước khi bắt đầu lướt
        yield return new WaitForSeconds(dashDelay);

        // Xác định hướng lướt theo trục X
        Vector3 dashDirection = new Vector3((player.position.x - transform.position.x) > 0 ? 1 : -1, 0, 0);
        Vector3 dashTarget = transform.position + dashDirection * dashDistance;

        FlipTowards(dashTarget); // Lật hướng về phía mục tiêu lướt

        float startTime = Time.time;
        while (Time.time < startTime + dashDistance / dashSpeed)
        {
            transform.position = Vector3.MoveTowards(transform.position, dashTarget, dashSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f); // Tạm dừng một lúc sau khi lướt

        // Quay lại vị trí ban đầu
        while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, patrolSpeed * Time.deltaTime
                        );

            yield return null;
        }

        isDashing = false;
        nextDashTime = Time.time + dashCooldown;
    }

    private void FlipTowards(Vector3 target)
    {
        if ((transform.position.x < target.x && transform.localScale.x < 0) || (transform.position.x > target.x && transform.localScale.x > 0))
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    // Vẽ phạm vi tuần tra và phạm vi phát hiện trong chế độ Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(pointA.position, pointB.position); // Vẽ đường tuần tra
        Gizmos.DrawWireSphere(pointA.position, 0.3f); // Vẽ điểm tuần tra A
        Gizmos.DrawWireSphere(pointB.position, 0.3f); // Vẽ điểm tuần tra B

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Vẽ phạm vi phát hiện
    }
}