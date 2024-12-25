using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrap : MonoBehaviour
{
    public int jumpForce = 30;
    public Rigidbody2D rb;
    public GameObject Effect;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            rb = player.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component not found on Player object!");
            }
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            GameObject newEffect = Instantiate(Effect, transform.position, Quaternion.identity);

            // Sử dụng GameManager để vô hiệu hóa và kích hoạt lại
            gameManager.DisableAndEnable(this.gameObject, 5f);
        }
    }
}
