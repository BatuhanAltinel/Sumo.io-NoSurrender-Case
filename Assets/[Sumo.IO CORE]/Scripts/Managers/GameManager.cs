using System;


public enum GameState 
{ 
    ReadyToStartGame, 
    OnTimer,
    InGame,
    End 

}
public class GameManager : Singleton<GameManager>
{
    

    public GameState _gameState = GameState.ReadyToStartGame;

    void OnEnable()
    {
        EventManager.OnGameStateChanged += SetGameState;
    }

    void OnDisable()
    {
        EventManager.OnGameStateChanged -= SetGameState;
    }

    void Start()
    {
        EventManager.OnGameStateChanged.Invoke(_gameState);
    }

	public bool IsState(GameState state)
    {
        return _gameState == state;
    }

    private void SetGameState(GameState state)
    {
        _gameState = state;
    }
}