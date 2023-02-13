using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;
    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Unpause();
            }
            else if (!paused)
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);

    }

    public void Unpause()
    {
        paused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);

    }

    public void OpenSettings()
    {
        // Settings haven't been implemented yet
    }

    public void Exit()
    {
        Application.Quit();
    }

}
