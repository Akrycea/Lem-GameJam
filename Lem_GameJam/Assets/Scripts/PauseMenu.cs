using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] MaskSlider script;

    [SerializeField] GameObject pauseMenu;
    public Button resumeButton;
   
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
        if (isPaused)
        {
            return; 
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false && canPause == true && script.isDead == false)
        {
            PauseGame();
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

        resumeButton.interactable = true;
    }

    public void ResumeGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        canPause = true;

        resumeButton.interactable = false;
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
