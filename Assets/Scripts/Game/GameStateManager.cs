using UnityEngine;

public enum GameState
{
    Playing,
    Diagnostic,
    Helpdesk,
    Dialogue,
    Paused
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public GameState CurrentState { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            CurrentState = GameState.Playing;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetState(GameState newState)
    {
        CurrentState = newState;
    }

    public bool IsPlaying()
    {
        return CurrentState == GameState.Playing;
    }
}