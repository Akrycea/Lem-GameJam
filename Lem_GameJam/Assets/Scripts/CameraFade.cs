using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFade : MonoBehaviour
{
    public static CameraFade Instance; 

    public float speedScale = 1f;
    public Color fadeColor = Color.black;
    public AnimationCurve Curve = new AnimationCurve(new Keyframe(0, 1),
    new Keyframe(0.5f, 0.5f, -1.5f, -1.5f), new Keyframe(1, 0));
    public bool startFadedOut = false;
    public bool fade = false;

    private float alpha = 1f; 
    private Texture2D texture;
    private int direction = 0;
    private float time = 0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //if (startFadedOut) alpha = 1f; else alpha = 0f;

        alpha = 1f;
        time = 0f;
        direction = 1;

        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
        texture.Apply();
    }

    void Update()
    {
        if (direction == 0 && fade == true)
        {
            if (alpha >= 1f) // Fully faded out
            {
                alpha = 1f;
                time = 0f;
                direction = 1;

                fade = false;
            }

            else if (alpha == 0) // Fully faded in
            {
                alpha = 0f;
                time = 1f;
                direction = -1;

                fade = false;
            }
                 
        }
    }

    public void OnGUI()
    {
        if (alpha > 0f) GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);

        if (direction != 0)
        {
            time += direction * Time.deltaTime * speedScale;
            alpha = Curve.Evaluate(time);
            texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
            texture.Apply();

            if (alpha <= 0f || alpha >= 1f) direction = 0;
        }
    }

    public void TriggerFade()
    {
        fade = true;
    }
}

