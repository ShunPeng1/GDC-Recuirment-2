using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    #region Instance

    protected static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType< UIManager>();
            if (_instance == null)
            {
                _instance = new GameObject("Spawned WorldTasksManager", typeof(UIManager)).GetComponent <UIManager>();
            }

            return _instance;
        }
        set
        {
            _instance = value;    
        }
    }
    #endregion

    
    #region Coins

    [Header("Coins")]
    [SerializeField] private Image coinsBar;
    [SerializeField] private TextMeshProUGUI coinsBarText;
    [SerializeField] private TextMeshProUGUI coinsText;

    public void UpdateCoins(float coins = -1, float coinsRequirement = -1)
    {
        if (coinsRequirement > 0)
        {
            if(coinsBar != null) coinsBar.fillAmount = coins/coinsRequirement;
            if (coinsBarText != null) coinsBarText.text = coins.ToString() + " / " + coinsRequirement.ToString();
        }
        if(coinsText != null) coinsText.text = coins.ToString() ;
    }

    
    
    #endregion


    #region Health

    [Header("Health")] 
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    public void UpdateHealth(float currentHealth = 0, float maxHealth = 1)
    {
        
        if(healthBar != null) healthBar.fillAmount = currentHealth / maxHealth;
        if(healthText != null) healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    #endregion


    #region Stamina

    [Header("Stamina")] 
    [SerializeField] private Image staminaBar;
    [SerializeField] private TextMeshProUGUI staminaText;
    public void UpdateStamina(float currentStamina, float maxStamina)
    {
        if(staminaBar != null) staminaBar.fillAmount = currentStamina / maxStamina;
        if(staminaText != null) staminaText.text = currentStamina.ToString();
    }

    #endregion

    
    
    #region Day
    [Header("Day")]
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private string dayTextInit = "Day ";
    public void UpdateDay(int day)
    {
        if(dayText != null) dayText.text = dayTextInit + day.ToString() ;
    }

    #endregion

    
    
}
