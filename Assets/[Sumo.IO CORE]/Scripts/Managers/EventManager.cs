using System;
using UnityEngine;

public class EventManager
{
    public static Action<GameState> OnGameStateChanged;
    public static Action OnInputDisabled;
    public static Action OnInputEnabled;
    public static Action<Sumo> OnFeeding;
    public static Action<int> OnScoreUpdate;      // int: current score
    
}