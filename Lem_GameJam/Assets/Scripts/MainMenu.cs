using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string sceneName;
    [SerializeField] GameObject tutorial;
    bool tutorialEnabled;

    void Start()
    {
        Time.timeScale = 1f;
        
        MusicManager.Instance.PlayMusic("Ambient");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        tutorial.SetActive(false);
        tutorialEnabled = false;
        
        gameObject.GetComponent<Button>().onClick.AddListener(Tutorial); 
    }

    public void PlayGame()
    {
        CameraFade.Instance.TriggerFade();
        
        StartCoroutine(WaitToStart());
        
    }

    public void Tutorial()
    {
        tutorialEnabled ^= true;
        tutorial.SetActive(tutorialEnabled);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Cutscene");
    }
}
