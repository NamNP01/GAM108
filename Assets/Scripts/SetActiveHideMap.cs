using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveHideMap : MonoBehaviour
{
    public GameObject hideMap;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hideMap.SetActive(true);
        }
    }
}
