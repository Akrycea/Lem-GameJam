using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] MaskSlider script;
    [SerializeField] PauseMenu pause;

    public GameObject icon;
    public GameObject alarm;
    public GameObject maskSlider;
    public GameObject redFilter;
    public GameObject youDied;
    public GameObject youWon;
    public TMP_Text maskUI;
    public GameObject key;
    public bool canPickUp = false;
    public bool canTakeOff = false;
    public bool hasKey = false;
    public bool canOpen = false;
    public bool hasWon = false;
    public bool hasDied = false;
    public Animator animator;
    public bool isMasked = false;
    public bool exploded = false;

    public AudioSource maskOn;
    public AudioSource maskOff;
    public AudioSource robotDeath;
    public AudioSource keySound;
    public AudioSource keyDoor;
    public AudioSource door;

    void Start()
    {
        icon.SetActive(false);
        maskSlider.SetActive(false);
        redFilter.SetActive(false);
        youDied.SetActive(false);
        youWon.SetActive(false);
        script.maskOn = false; 
    }

    void Update()
    {
        animator.SetBool("Masked", isMasked);
        animator.SetBool("Death", exploded);

        if (pause.isPaused || script.isDead)
        {
            return;  
        }

        if (Input.GetKeyDown(KeyCode.Space) && script.maskOn == false && script.isDead == false)
        {
            maskOn.Play();
            maskSlider.SetActive(true);
            redFilter.SetActive(true);
            canTakeOff = true;
            isMasked = true;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && script.maskOn == true && canTakeOff == true && script.isDead == false)
        {
            maskOff.Play();
            isMasked = false;
            StartTimer();
        }

        if (Input.GetKeyDown(KeyCode.E) && canPickUp == true && script.isDead == false && pause.isPaused == false)
        {
            keySound.Play();
            key.SetActive(false);
            hasKey = true;
            Debug.Log("Key acquired");
        }

        if (Input.GetKeyDown(KeyCode.E) && canOpen == true && script.isDead == false && pause.isPaused == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            keyDoor.Play();
            door.Play();
            hasWon = true;
            pause.canPause = false;
            alarm.SetActive(false);
            youWon.SetActive(true);
            Time.timeScale = 0f;
        }

        if (canPickUp == true || canOpen == true)
        {
            icon.SetActive(true);
        }

        else
        {
            icon.SetActive(false);
        }

        if (script.isDead == true)
        {
            exploded = true;
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Key")
        {
            canPickUp = true;
        }

        if (other.tag == "Enemy" && isMasked == false)
        {
            // robotDeath.Play();
            Time.timeScale = 0f;
            
            hasDied = true;
            exploded = true;
            pause.canPause = false;
            script.isDead = true;

            youDied.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
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
        maskUI.faceColor = new Color32(255, 255, 255, 25);

        yield return new WaitForSeconds(3);

        script.maskOn = false;
        maskUI.faceColor = new Color32(255, 255, 255, 255);
    }
}
