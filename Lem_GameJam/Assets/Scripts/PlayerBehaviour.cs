using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] MaskSlider script;
    public GameObject maskSlider;
    public GameObject redFilter;
    public TMP_Text maskUI;
    public GameObject key;
    public bool canPickUp = false;
    public bool canTakeOff = false;
    public bool hasKey = false;
    public bool canOpen = false;

    void Start()
    {
        maskSlider.SetActive(false);
        redFilter.SetActive(false);
        script.maskOn = false; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && script.maskOn == false)
        {
            maskSlider.SetActive(true);
            redFilter.SetActive(true);
            canTakeOff = true;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && script.maskOn == true && canTakeOff == true)
        {
            StartTimer();
        }

        if (Input.GetKeyDown(KeyCode.E) && canPickUp == true)
        {
            key.SetActive(false);
            hasKey = true;
            Debug.Log("Key acquired");
        }

        if (Input.GetKeyDown(KeyCode.E) && canOpen == true)
        {
            Debug.Log("You won");
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Key")
        {
            canPickUp = true;
        }

        if (other.tag == "Enemy" && script.maskOn == false)
        {
            Debug.Log("You died");
        }

        if (other.tag == "Door" && hasKey == true)
        {
            canOpen = true;
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.tag == "Key")
        {
            canPickUp = false;
        }

        if (other.tag == "Door" && hasKey == false)
        {
            canOpen = false;
        }
    }

    public void StartTimer()
    {
        StartCoroutine(MaskColor());
    }

    IEnumerator MaskColor()
    {
        canTakeOff = false;
        maskSlider.SetActive(false);
        redFilter.SetActive(false);
        maskUI.faceColor = new Color32(255, 255, 255, 75);

        yield return new WaitForSeconds(3);

        script.maskOn = false;
        maskUI.faceColor = new Color32(255, 255, 255, 255);
    }
}
