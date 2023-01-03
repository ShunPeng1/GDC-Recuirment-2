using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : InstanceManager<GameHandler>
{
    public enum GameStates
    {
        Shopping,
        Occurring,
        Paused,
        Ended,
        Initializing
    }

    //[SerializeField] private GameStates gameStates = GameStates.Occurring;

    
}
