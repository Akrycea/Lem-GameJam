using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] PlayerBehaviour script;
    public SpriteRenderer spriteRenderer;
    public Sprite openSprite;
    public bool canEscape = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canEscape == true)
        {
            spriteRenderer.sprite = openSprite;
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player" && script.hasKey == true)
        {
            canEscape = true;
        }

        else
        {
            canEscape = false;
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            canEscape = false;
        }
    }
}
