using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.UpdateCoins(coins, GameHandler.Instance.GetCoinsRequirement());    
    }

    #region Health

    [Header("Health")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float curHealth = 100;
    
    public void TakeDamage(float damageTaken)
    {
        if (damageTaken <= 0) return;
        curHealth = curHealth - damageTaken ;

        curHealth = Mathf.Min(maxHealth, curHealth);
        curHealth = Mathf.Max(0, curHealth);

        if (curHealth == 0)
        {
            Debug.Log("Game over");
        }
    }
    
    public void Heal(float healAmount)
    {
        if (healAmount <= 0) return;
        curHealth = curHealth + healAmount ;

        curHealth = Mathf.Min(maxHealth, curHealth);
        curHealth = Mathf.Max(0, curHealth);

    }

    
    #endregion


    [Header("Attack Damage")] 
    [SerializeField] private float damage = 0;

    #region Coins

    [Header("Coins")]
    [SerializeField] private float coins = 0;

    public void AddCoins(float addition)
    {
        coins += addition;
        UIManager.Instance.UpdateCoins(coins, GameHandler.Instance.GetCoinsRequirement());
    }

    public bool SpendCoins(float spending, bool updateCoinsBar = true)
    {
        if (spending > coins || spending < 0) return false;

        coins -= spending;
        if(updateCoinsBar) UIManager.Instance.UpdateCoins(coins, GameHandler.Instance.GetCoinsRequirement());
        else UIManager.Instance.UpdateCoins(coins);
        return true;
    }

    public float GetCoins()
    {
        return coins;
    }
    public void SetCoins(float coinsAssign)
    {
        coins =  coinsAssign;
        UIManager.Instance.UpdateCoins(coins, GameHandler.Instance.GetCoinsRequirement());
    }
    #endregion
    
    
    
    
}
