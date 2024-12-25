﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    public int levelValue;
    public int scoreValue;
    public float currentTime;
    public float finalTime;
    // Thêm các trường dữ liệu khác của người chơi tại đây

    
}