using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
   
    // [SerializeField] AudioSource music;
    public bool isPaused;
    public bool canPause;
    public string sceneName;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1f;

        isPaused = false;
        canPause = true;
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false && canPause == true)
        {
            PauseGame();
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        canPause = false;

        // MusicManager.Instance.volume = 0.05f;
    }

    public void ResumeGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        canPause = true;

       // MusicManager.Instance.volume = 0.1f;
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
