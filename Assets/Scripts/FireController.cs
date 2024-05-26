using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public GameObject Effect;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
        player = GameObject.FindGameObjectWithTag("Player").transform; 

        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }
        GameObject newBullet = Instantiate(Effect, transform.position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
