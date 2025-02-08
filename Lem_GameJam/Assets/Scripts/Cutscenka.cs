using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscenka : MonoBehaviour
{
    public GameObject canvas;

    void Start()
    {
        canvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        StartCoroutine(wait());

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(2);
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
        canvas.SetActive(true);
        yield return new WaitForSeconds(9);
        SceneManager.LoadScene(2);
    }


}
