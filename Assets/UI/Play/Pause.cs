using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pause : MonoBehaviour
{
    public static float GameStop = 1;

    [SerializeField]
    private Image Pause_Background;

    [SerializeField]
    private Image Pause_BackImage;

    [SerializeField]
    private Text Pause_Text;

    [SerializeField]
    private Button Pause_BackToGame_Button;
    
    [SerializeField]
    private Button Pause_Title_Button;

    // Start is called before the first frame update
    void Start()
    {
        Pause_Background.enabled = false;
        Pause_BackImage.enabled = false;
        Pause_Text.enabled = false;
        Pause_BackToGame_Button.interactable = false;
        Pause_BackToGame_Button.gameObject.SetActive(false);
        Pause_Title_Button.interactable = false;
        Pause_Title_Button.gameObject.SetActive(false);
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameStop == 0){
            ResumeGame();
            Pause_Background.enabled = false;
            Pause_BackImage.enabled = false;
            Pause_Text.enabled = false;
            Pause_BackToGame_Button.interactable = false;
            Pause_BackToGame_Button.gameObject.SetActive(false);
            Pause_Title_Button.interactable = false;
            Pause_Title_Button.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && GameStop == 1){
            PauseGame();
            Pause_Background.enabled = true;
            Pause_BackImage.enabled = true;
            Pause_Text.enabled = true;
            Pause_BackToGame_Button.interactable = true;
            Pause_BackToGame_Button.gameObject.SetActive(true);
            Pause_Title_Button.interactable = true;
            Pause_Title_Button.gameObject.SetActive(true);
        }
    }
}