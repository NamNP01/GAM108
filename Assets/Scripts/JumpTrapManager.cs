using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void DisableAndEnable(GameObject obj, float delay)
    {
        StartCoroutine(DisableAndEnableCoroutine(obj, delay));
    }

    private IEnumerator DisableAndEnableCoroutine(GameObject obj, float delay)
    {
        obj.SetActive(false);
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
    }
}
