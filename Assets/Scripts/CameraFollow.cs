using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 0.125f;
    private Transform target;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null || !target.gameObject.activeSelf)
        {
            // Tìm kiếm một nhân vật mới để theo dõi
            FindNewTarget();
        }
        else
        {
            // Di chuyển camera đến vị trí của nhân vật mà không thay đổi trục z
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    private void FindNewTarget()
    {
        // Tìm kiếm các nhân vật hoạt động trong trò chơi và chọn một nhân vật mới để theo dõi
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject character in characters)
        {
            if (character.activeSelf)
            {
                target = character.transform;
                break;
            }
        }

        // Nếu không có nhân vật nào còn hoạt động, không làm gì cả
        if (target == null)
        {
            return;
        }

        // Di chuyển camera đến vị trí của nhân vật mới mà không thay đổi trục z
        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
