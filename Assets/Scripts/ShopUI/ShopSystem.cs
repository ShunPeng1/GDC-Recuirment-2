using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShopSystem : MonoBehaviour
{
    [Serializable]
    public class ShopItem
    {
        public Item item;
        public Image fillBar;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI upgradeCostText;
    }
    
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private List<ShopItem> _shopItems; 

    void Start()
    {
        if (player == null) player = GameObject.Find("Player");

        for (int i = 0; i < _shopItems.Count; i++)
        {
            UpgradeDisplay(i, 0);
        }

        infoText.text = "Item upgrade info";

    }

    public void OnUpgradeItem(int index)
    {
        var playerStats = player.GetComponent<PlayerStats>();
        var currentCoins = playerStats.GetCoins();
        float coinsSpend = _shopItems[index].item.TryUpgrade(currentCoins); 
        if ( coinsSpend == -1)
        {
            Debug.Log( "Not enough coins" );
            return;
        }
        else if (coinsSpend == -2)
        {
            Debug.Log("Max level");
            return;
        }

        UpgradeDisplay(index,0);
        playerStats.SpendCoins(coinsSpend);
        Debug.Log("Upgrade Item");
    }

    private void UpgradeDisplay(int index, int levelOffset)
    {
        ShopItem shopItem = _shopItems[index];
        
        shopItem.item.GetNumberOfUpgrade(out int currentLevel, out int maxLevel, out string upgradeInfo);
        shopItem.fillBar.fillAmount = (float)currentLevel / (float)maxLevel;
        shopItem.levelText.text = currentLevel.ToString() + "/" +maxLevel.ToString();

        infoText.text = upgradeInfo;
        
        if (currentLevel != maxLevel)
        {
            shopItem.upgradeCostText.text = shopItem.item.GetUpgradeCost(levelOffset).ToString();
        }
        else
        {
            shopItem.upgradeCostText.text = "MAX";
        }
        
        
        
    }
}
