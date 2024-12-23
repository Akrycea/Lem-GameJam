using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskSlider : MonoBehaviour
{
    public GameObject youDied;
    public Slider timeSlider;
    public float sliderTimer;
    public bool stopTimer = false;
    public bool maskOn = false;
    public bool isDead = false;

    void Start()
    {
        timeSlider = GetComponent<Slider>();
        youDied.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isDead == true)
        {
            Application.Quit();
        }
    }

    void OnEnable()
    {
        maskOn = true;

        sliderTimer = 7;
        timeSlider.maxValue = sliderTimer;
        timeSlider.value = sliderTimer;

        StartTimer();
    }

    public void StartTimer()
    {
        StartCoroutine(MaskTicker());
    }

    IEnumerator MaskTicker()
    {
        while (stopTimer == false)
        {
            sliderTimer -= Time.deltaTime;
            yield return new WaitForSeconds(0.001f);

            if (sliderTimer <= 0)
            {
                stopTimer = true;

                youDied.SetActive(true);
                isDead = true;
                
                Time.timeScale = 0f;
            }

            if (stopTimer == false)
            {
                timeSlider.value = sliderTimer;
            }
        }
    
    }
}
