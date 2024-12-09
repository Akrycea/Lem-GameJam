using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskSlider : MonoBehaviour
{
    public Slider timeSlider;
    public float sliderTimer;
    public bool stopTimer = false;
    public bool maskOn = false;

    void Start()
    {
        timeSlider = GetComponent<Slider>();
    }

    void OnEnable()
    {
        timeSlider.maxValue = sliderTimer;
        timeSlider.value = sliderTimer;

        maskOn = true;

        StartTimer();
    }

    void OnDisable()
    {
        timeSlider.maxValue = sliderTimer;
        timeSlider.value = sliderTimer;

        maskOn = false;
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

                Debug.Log("You died");
                //DEATH SCREEN
            }

            if (stopTimer == false)
            {
                timeSlider.value = sliderTimer;
            }
        }
    
    }
}
