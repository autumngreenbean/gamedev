using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles main menu functionality and control logic.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OptionsMenuOpen()
    {
        // Still have to implement
    }

    public void CreditMenuOpen()
    {
        // Still have to implement
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame("Main menu quit button pressed.");
    }
}
