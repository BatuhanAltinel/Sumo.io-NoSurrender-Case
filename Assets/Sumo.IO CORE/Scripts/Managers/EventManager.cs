using System;
using UnityEngine;

public class EventManager
{
    public static Action OnGameStarted;
    public static Action OnInputDisabled;
    public static Action OnInputEnabled;
    public static Action<int> OnScoreChanged;      // int: current score
    
}