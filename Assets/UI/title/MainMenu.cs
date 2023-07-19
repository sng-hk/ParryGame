using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Image Setting_Image;

    [SerializeField]
    private Button Setting_Close;

    [SerializeField]
    private Text Setting_Text;

    [SerializeField]
    private Text Setting_Sound_Text;

    [SerializeField]
    private Text Setting_Sound_Value;

    [SerializeField]
    private Slider Sound_Bar;

    int Sound_Value;

    // Start is called before the first frame update
    void Start()
    {
        Setting_Image.enabled = false;
        Setting_Close.interactable = false;
        Setting_Close.gameObject.SetActive(false);
        Setting_Text.enabled = false;
        Setting_Sound_Text.enabled = false;
        Setting_Sound_Value.enabled = false;
        Sound_Bar.interactable = false;
        Sound_Bar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TextChanger(float value)
    {
        Sound_Value = (int)(value*100);
        Setting_Sound_Value.text = Sound_Value.ToString();
    }

    public void OnClickNewGame()
    {
        SceneManager.LoadScene("loading");
    }

    public void OnClickLoad()
    {

    }

    public void OnClickOption()
    {
        Setting_Image.enabled = true;
        Setting_Close.interactable = true;
        Setting_Close.gameObject.SetActive(true);
        Setting_Text.enabled = true;
        Setting_Sound_Text.enabled = true;
        Setting_Sound_Value.enabled = true;
        Sound_Bar.interactable = true;
        Sound_Bar.gameObject.SetActive(true);
    }

    public void Setting_Close_Button()
    {
        Setting_Image.enabled = false;
        Setting_Close.interactable = false;
        Setting_Close.gameObject.SetActive(false);
        Setting_Text.enabled = false;
        Setting_Sound_Text.enabled = false;
        Setting_Sound_Value.enabled = false;
        Sound_Bar.interactable = false;
        Sound_Bar.gameObject.SetActive(false);
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
