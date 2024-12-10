using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscenka : MonoBehaviour
{

    private void Update()
    {
        StartCoroutine(wait());
        
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene(2);
    }


}
