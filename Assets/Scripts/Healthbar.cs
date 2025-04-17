using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image healthbar;

    public void UpdateHealthbar(int maxHealth, int health)
    {
        healthbar.fillAmount = (float)health / maxHealth;
    }
}
