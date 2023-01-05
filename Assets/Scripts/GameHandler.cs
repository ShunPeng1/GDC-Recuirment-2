using System;
using System.Collections;
using System.Collections.Generic;
using TurnTheGameOn.Timer;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    #region Instance

    protected static GameHandler _instance;

    public static GameHandler Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType< GameHandler>();
            if (_instance == null)
            {
                _instance = new GameObject("Spawned WorldTasksManager", typeof(GameHandler)).GetComponent <GameHandler>();
            }

            return _instance;
        }
        set
        {
            _instance = value;    
        }
    }
    #endregion

    [Header("Player")] [SerializeField] private GameObject player;
    
    [Header("Shop")]
    [SerializeField] private GameObject shopUI;

    
    [Header("Day")] 
    [SerializeField] private int currentDay = 0;
    [SerializeField] private float secondPerDay = 300;
    [SerializeField] private Timer timer;

    [Header("Coins Requirement")] 
    [SerializeField] private float coinsRequirement = 100;
    [SerializeField] private float coinsScale = 1.5f; 
    
    public void Start()
    {
        EndDay();
    }

    public void ToPause()
    {
        Time.timeScale = 0;
    }
    
    public void ToUnpause()
    {
        Time.timeScale = 1;
    }

    public void EndDay()
    {
        shopUI.SetActive(true);
        ToPause();
        timer.timerState = TimerState.Disabled;

        float playerCoins= player.GetComponent<PlayerStats>().GetCoins();
        if (playerCoins >= coinsRequirement)
        {
            player.GetComponent<PlayerStats>().SetCoins(playerCoins - coinsRequirement);
            coinsRequirement =10 * (int)Mathf.Round(coinsRequirement * coinsScale)/10f;
            UIManager.Instance.UpdateCoins(playerCoins - coinsRequirement, coinsRequirement);
        }
        else
        {
            Debug.Log("Game over");
        }
        
    }

    public void NewDay()
    {
        shopUI.SetActive(false);
        ToUnpause();
        
        currentDay++;
        UIManager.Instance.UpdateDay(currentDay);
        
        timer.timerState = TimerState.Counting;
        timer.SetTimerValue(secondPerDay);
        
    }

    public float GetCoinsRequirement()
    {
        return coinsRequirement;
    }

   
}
