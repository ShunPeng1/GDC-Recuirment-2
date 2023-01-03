using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;
public class UIManager : InstanceManager<UIManager>
{
    [Header("Coins")] 
    [SerializeField] private TextMeshProUGUI coinsText;

    public void UpdateCoins(float coins)
    {
        coinsText.text = coins.ToString() ;
    }
    
    
    
}
