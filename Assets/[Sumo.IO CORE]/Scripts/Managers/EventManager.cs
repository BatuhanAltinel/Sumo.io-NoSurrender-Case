using System;
using UnityEngine;

public class EventManager
{
    public static Action<GameState> OnGameStateChanged;
    public static Action OnSumoFallDown;
    public static Action<int> OnScoreUpdate;      // int: current score
    
}