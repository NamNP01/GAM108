using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalPoint : MonoBehaviour
{
    public Text ScoreText;
    public Text TimingText;
    public PlayerData Data;
    // Start is called before the first frame update
    void Start()
    {
        Data.scoreValue = PlayerPrefs.GetInt("PlayerScore");
        Data.finalTime = PlayerPrefs.GetFloat("Playertime");


        ScoreText.text = " " + Data.scoreValue.ToString();

        int minutes = Mathf.FloorToInt(Data.finalTime / 60);
        int seconds = Mathf.FloorToInt(Data.finalTime % 60);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        TimingText.text = "" + timeString;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
