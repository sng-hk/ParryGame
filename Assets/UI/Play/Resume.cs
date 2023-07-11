using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resume : MonoBehaviour
{
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnclickResumeButton()
    {
        Pause.ResumeGame();
        Pause_Background.enabled = false;
        Pause_BackImage.enabled = false;
        Pause_Text.enabled = false;
        Pause_BackToGame_Button.interactable = false;
        Pause_BackToGame_Button.gameObject.SetActive(false);
        Pause_Title_Button.interactable = false;
        Pause_Title_Button.gameObject.SetActive(false);
    }
}
