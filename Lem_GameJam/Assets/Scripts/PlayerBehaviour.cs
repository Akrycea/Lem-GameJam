using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] MaskSlider script;
    public GameObject maskSlider;

    void Start()
    {
        maskSlider.SetActive(false);
        script.maskOn = false; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && script.maskOn == false)
        {
            maskSlider.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.Space) && script.maskOn == true)
        {
            maskSlider.SetActive(false);
        }
    }
}
