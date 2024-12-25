using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRock : MonoBehaviour
{
    public Animator ani;
    public Rigidbody2D rb;
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
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            ani.SetTrigger("Active");
            StartCoroutine(DisableAfterDelay(1f)); 
        }
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        this.gameObject.SetActive(false); 
        gameManager.DisableAndEnable(this.gameObject, 5f);
    }
}
