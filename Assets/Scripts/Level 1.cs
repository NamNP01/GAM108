using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public PlayerData playerData;
    void Start()
    {
        playerData.finalTime = 0;
        PlayerPrefs.SetInt("PlayerLevel", 1);
        PlayerPrefs.SetInt("PlayerScore", 0);
        PlayerPrefs.SetInt("Playertime", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
