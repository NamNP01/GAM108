using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterZone : MonoBehaviour
{
    public float waterGravityScale = 0.5f; // Trọng lực khi ở trong nước
    public float normalGravityScale = 1f;  // Trọng lực bình thường
    public float waterJumpReduction = 0.5f; // Hệ số giảm lực nhảy khi ở trong nước

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Lấy Rigidbody2D của player và thay đổi trọng lực
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = waterGravityScale;
                MainCharater mainChar = collision.GetComponent<MainCharater>();
                if (mainChar != null)
                {
                    mainChar.AdjustJumpForce(waterJumpReduction);
                    mainChar.isInWater = true; // Đánh dấu là nhân vật đang ở trong nước
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Lấy Rigidbody2D của player và đặt lại trọng lực bình thường
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = normalGravityScale;
                MainCharater mainChar = collision.GetComponent<MainCharater>();
                if (mainChar != null)
                {
                    mainChar.ResetJumpForce();
                    mainChar.isInWater = false; // Đánh dấu là nhân vật đã ra khỏi nước
                }
            }
        }
    }
}
