using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMap : MonoBehaviour
{
    public int lockKeyCount = 0; // Số lượng lockKey đã thu thập
    public int requiredLockKeys1 = 3;
    public GameObject hiddenMap1;
    public int requiredLockKeys2 = 6;
    public GameObject hiddenMap2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu đối tượng va chạm là lockKey
        if (collision.gameObject.CompareTag("LockKey"))
        {
            CollectLockKey();
            Destroy(collision.gameObject,0.3f); // Hủy đối tượng lockKey
        }
    }

    public void CollectLockKey()
    {
        lockKeyCount++;
        if (lockKeyCount == requiredLockKeys1 && hiddenMap1 != null && hiddenMap1.activeSelf)
        {
            hiddenMap1.SetActive(false);
        }
        if (lockKeyCount == requiredLockKeys2 && hiddenMap2 != null && hiddenMap2.activeSelf)
        {
            hiddenMap2.SetActive(false);
        }
    }
}
