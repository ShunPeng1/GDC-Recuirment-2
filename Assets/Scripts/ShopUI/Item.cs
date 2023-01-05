using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Timer = TurnTheGameOn.Timer.Timer;


public class Item : MonoBehaviour
{
    [Serializable]
    public class ItemLevel
    {
        public float costToUpgrade;
    }

    [Header("Child GO ")]
    [SerializeField] protected GameObject _cooldownGameObject;
    [SerializeField] protected GameObject _selectingGameObject;

    [Header("Upgrade Level")]
    [SerializeField] protected int currentLevel = 0;
    [SerializeField] protected List<ItemLevel> levels;
    [SerializeField] protected string upgradeInfo = "Not implement yet";
    
    [Header("Cooldown")]
    [SerializeField] protected float cooldown = 1;
    [SerializeField] private Timer timer;

    protected void Start()
    {
        if(_selectingGameObject == null) _selectingGameObject = transform.GetChild(0).gameObject;
        if(_cooldownGameObject == null) _cooldownGameObject = transform.GetChild(3).gameObject;
        AfterStart();
    }

    protected virtual void AfterStart()
    {
        
    }

    public virtual void OnUsingItem()
    {
        Debug.Log("Item used");
    }

    public virtual void OnSelect()
    {
        _selectingGameObject.SetActive(true);
    }

    public virtual void OnDeselect()
    {
        _selectingGameObject.SetActive(false);
    }

    public virtual void GetNumberOfUpgrade(out int levelCurrently , out int maxLevel, out string infoText)
    {
        levelCurrently = currentLevel; //count from 0 with 0 is the init state 
        maxLevel = levels.Count;
        infoText = upgradeInfo;
    }
    public virtual float GetUpgradeCost(float levelOffset) //return upgraded cost
    {
        if (currentLevel + levelOffset > levels.Count) 
        {
            return -1;
        }

        return levels[(int)(currentLevel + levelOffset)].costToUpgrade;

    }
    public virtual float TryUpgrade(float currentCoins) //return upgraded cost
    {
        if (currentLevel >= levels.Count) 
        {
            return -2;
        }

        if (currentCoins < levels[currentLevel].costToUpgrade)
        {
            return -1;
        }
        
        Upgrade();
        currentLevel++;
        return levels[currentLevel-1].costToUpgrade;

    }

    protected virtual void Upgrade()
    {
        
    }
    
    
}
