using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private void Start()
    {
        curHealth = maxHealth;
    }

    #region Health

    [Header("Health")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float curHealth;

    
    
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
    
    #endregion


    [Header("Attack Damage")] 
    [SerializeField] private float damage;
    
    [Header("Money")]
    [SerializeField] private float coins;

    public void AddMoney(float addition)
    {
        coins += addition;
    }

    public bool SpendMoney(float spending)
    {
        if (spending > coins || spending < 0) return false;

        coins -= spending;
        return true;
    }
    
}
