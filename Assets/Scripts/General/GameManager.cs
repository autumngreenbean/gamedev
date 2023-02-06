using System;
using UnityEngine;

/// <summary>
/// All possible game states.
/// </summary>
public enum GameState
{
    Initial,
    MainMenu,
    Settings,
    Gameplay, // TODO: Break up when gameplay details determined
    Paused,
}

/// <summary>
/// Controls high-level game logic.
/// </summary>
public class GameManager : MonoBehaviour
{
    //////////////////////////////////////////////////
    // Public Properties and Methods //
    //////////////////////////////////////////////////

    public static GameManager Instance { get; set; } // Singleton class

    public void PauseGame()
    {
        Time.timeScale = 0f;
        SetState(GameState.Paused);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        SetState(lastState);
    }

    public void SaveGame()
    {
        throw new NotImplementedException();
    }

    public void QuitGame(String message = "")
    {
        Debug.Log(String.Format("{0}\nGame Quit.\n", message));
        Application.Quit();
    }

    public void FailGame(bool fatal, String message)
    {
        Debug.Log(String.Format("Error: {0}\n", message));

        if (fatal)
            QuitGame("Fatal error.");
        else
            Debug.Log("Non-fatal error. Attempting to continue...");
    }

    //////////////////////////////////////////////////
    // Private Fields and Methods //
    //////////////////////////////////////////////////

    private GameState lastState = GameState.Initial;
    private GameState state = GameState.Initial;

    private void Awake()
    {
        if (Instance != null)
        {
            FailGame(true, "Multiple GameManagers detected.");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SetState(GameState.Gameplay);
    }

    private void Update() { }

    private void SetState(GameState newState)
    {
        lastState = state;
        state = newState;
    }
}
