using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoomerang : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public float maxChargeTime = 1.0f; // Thời gian tối đa để nạp lực bắn
    public float maxArrowSpeed = 50.0f; // Tốc độ tối đa của mũi tên
    public float shootingAngle = 45f; // Góc bắn (độ)

    private float currentChargeTime = 0.0f;

    public GameObject pointPrefab; // Prefab cho các điểm biểu diễn hướng bắn
    GameObject[] points;
    public int numberOfPoints = 10;
    public float spaceBetweenPoints = 0.1f;

    private MainCharater mainCharater; // Tham chiếu tới script MainCharater


    public AudioSource src;
    public AudioClip Attack;

    void Start()
    {
        mainCharater = GetComponent<MainCharater>(); // Lấy tham chiếu tới MainCharater script

        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, arrowSpawnPoint.position, Quaternion.identity);
            points[i].SetActive(false); // Ẩn các điểm ban đầu
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.J))
        {
            // Tăng thời gian nạp lực bắn khi giữ phím J
            currentChargeTime += Time.deltaTime;
            if (currentChargeTime > maxChargeTime)
            {
                currentChargeTime = maxChargeTime;
            }

            // Hiển thị hướng bắn
            ShowTrajectory();
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            // Bắn mũi tên khi thả phím J

            src.clip = Attack;
            src.Play();
            ShootArrow();
            currentChargeTime = 0.0f;
            HideTrajectory(); // Ẩn các điểm biểu diễn hướng bắn
        }
    }

    void ShowTrajectory()
    {
        float arrowSpeed = (currentChargeTime / maxChargeTime) * maxArrowSpeed;
        float radianAngle = shootingAngle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(radianAngle) * mainCharater.transform.localScale.x, Mathf.Sin(radianAngle));
        Vector2 velocity = direction * arrowSpeed;

        for (int i = 0; i < numberOfPoints; i++)
        {
            float time = i * spaceBetweenPoints;
            Vector2 position = (Vector2)arrowSpawnPoint.position + velocity * time + 0.5f * Physics2D.gravity * time * time;
            points[i].transform.position = position;
            points[i].SetActive(true);
        }
    }

    void HideTrajectory()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].SetActive(false);
        }
    }

    void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
        Boomerang arrowScript = arrow.GetComponent<Boomerang>();

        float BoomerangSpeed = (currentChargeTime / maxChargeTime) * maxArrowSpeed;
        float radianAngle = shootingAngle * Mathf.Deg2Rad;
        Vector2 arrowVelocity = new Vector2(BoomerangSpeed * Mathf.Cos(radianAngle) * mainCharater.transform.localScale.x, BoomerangSpeed * Mathf.Sin(radianAngle));

        arrowScript.Shoot(arrowVelocity);
    }
}
