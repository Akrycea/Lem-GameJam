using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneName;

    void Start()
    {
        MusicManager.Instance.PlayMusic("Ambient");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Cutscene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
