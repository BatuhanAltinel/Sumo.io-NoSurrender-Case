using System;


public enum GameState 
{ 
    ReadyToStartGame, 
    InGame,
    End 

}
public class GameManager : Singleton<GameManager>
{
    

    public GameState _gameState = GameState.ReadyToStartGame;

	public bool IsState(GameState state)
    {
        return _gameState == state;
    }

    private void SetGameState(GameState state)
    {
        _gameState = state;
    }
}