using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTrap : MonoBehaviour
{
    public Sprite newSprite;
    private SpriteRenderer spriteRenderer;
    public GameObject HidePlatForm;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Lấy SpriteRenderer của đối tượng
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Thay đổi sprite thành sprite mới
            if (spriteRenderer != null && newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
                HidePlatForm.SetActive(true);

            }
        }
        if (collision.CompareTag("Attack"))
        {
            // Thay đổi sprite thành sprite mới
            if (spriteRenderer != null && newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
                HidePlatForm.SetActive(true);

            }
        }
    }
}
