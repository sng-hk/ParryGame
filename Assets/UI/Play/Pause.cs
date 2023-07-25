using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pause : MonoBehaviour
{
    public static float GameStop = 1;

    [SerializeField]
    private GameObject pause_object;

    public Text pause_sound_value_text;

    int sound_salue;


    public void PauseEnable()
    {
        //일시정지 창 켜기.
        pause_object.SetActive(true);
    }

    public void PauseDisable()
    {
        //일시정지 창 끄기.
        pause_object.SetActive(false);
    }

    void Start()
    {
        PauseDisable();
    }

    public static void PauseGame()
    {
        Time.timeScale = 0;
        GameStop = 0;
    }
    public static void ResumeGame()
    {
        Time.timeScale = 1;
        GameStop = 1;
    }

    public void TextChanger(float value)
    {
        sound_value = (int)(value * 100);
        pause_sound_value_text.text = sound_value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (GameStop == 0)
            {
                ResumeGame();
                PauseDisable();
            }
            else if (GameStop == 1)
            {
                PauseGame();
                PauseEnable();
            }
        }
    }
}