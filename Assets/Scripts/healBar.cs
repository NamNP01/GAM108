using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image HP;


    public void UpdateHealth(int health, int maxHealth)
    {
        HP.fillAmount = (float)health / (float)maxHealth;
    }

}