using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrap : MonoBehaviour
{
    public GameObject Arrow;
    private bool isFallen;
    public Animator ani;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isFallen)
        {
            GameObject Fire = Instantiate(Arrow, transform.position, Quaternion.identity);
            ani.SetTrigger("Active");
            isFallen = true;
            StartCoroutine(SetIsFallenFalseAfterDelay(5f));
        }
    }

    private IEnumerator SetIsFallenFalseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        isFallen = false; 
        ani.SetTrigger("Reload"); 
    }
}
