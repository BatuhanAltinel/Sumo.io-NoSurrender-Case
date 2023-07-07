using System;

public class GameManager : Singleton<GameManager>
{
    public enum GameState { 
        ReadyToStartGame, 
        InGame,
        End 
        }

    public GameState _gameState = GameState.ReadyToStartGame;

	public bool IsInGame()
    {
        return _gameState == GameState.InGame;
    }

    public void StartGame()
    {
        _gameState = GameState.InGame;
        EventManager.OnGameStarted?.Invoke();
    }
}