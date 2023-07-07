using System;
using UnityEngine;

public class EventManager
{
    public static Action<GameState> OnGameStateChanged;
    public static Action OnInputDisabled;
    public static Action OnInputEnabled;
    public static Action<int> OnScoreChanged;      // int: current score
    
}