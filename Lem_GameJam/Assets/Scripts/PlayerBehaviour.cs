using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] MaskSlider script;
    public GameObject maskSlider;
    public TMP_Text maskUI;
    public GameObject key;
    public bool canPickUp = false;
    public bool canTakeOff = false;
    public bool hasKey = false;

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
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.tag == "Key")
        {
            canPickUp = false;
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
        maskUI.faceColor = new Color32(255, 255, 255, 75);

        yield return new WaitForSeconds(3);

        script.maskOn = false;
        maskUI.faceColor = new Color32(255, 255, 255, 255);
    }
}
