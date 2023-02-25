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
    ShipControl,
    Combat,
    Paused,
}

/// <summary>
/// Controls high-level game logic.
/// </summary>
/// <remarks>
/// One GameManager should be created in the first game scene. It will persist between scenes and no more should be created.
/// </remarks>
public class GameManager : MonoBehaviour
{
    //////////////////////////////////////////////////
    // Public Properties and Methods //
    //////////////////////////////////////////////////

    public static GameManager Instance; // Singleton class

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

    /// <summary>
    /// Called at game fail states. If a fatal error is detected, it will close the application.
    /// </summary>
    /// <remarks>
    /// Fatal errors will not close the game in the Unity Editor.
    /// </remarks>
    public void FailGame(bool fatal, String message)
    {
        Debug.Log(String.Format("Error: {0}\n", message));

        if (fatal)
            QuitGame("Fatal error.");
        else
            Debug.Log("Non-fatal error. Attempting to continue...");
    }

    public GameState GetState()
    {
        return state;
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
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SetState(GameState.ShipControl);
    }

    private void Update() { }

    private void SetState(GameState newState)
    {
        lastState = state;
        state = newState;
    }
}
