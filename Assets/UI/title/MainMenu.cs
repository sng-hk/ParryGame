using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject setting_object;

    public Text setting_sound_value_text;

    int sound_value;

    public void SettingEnable()
    {
        //세팅창 켜기.
        setting_object.SetActive(true);
    }

    public void SettingDisable()
    {
        //세팅창 끄기.
        setting_object.SetActive(false);
    }


    void Start()
    {
        SettingDisable();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //esc누르면 세팅창 닫히도록.
            SettingDisable();
        }
    }

    public void TextChanger(float value)
    {
        //사운드 크기에 맞춰 변하는 텍스트.
        sound_value = (int)(value*100);
        setting_sound_value_text.text = sound_value.ToString();
    }

    public void ClickNewGameButton()
    {
        //New Game 버튼 클릭 시.
        SceneManager.LoadScene("loading");
    }

    public void ClickContinueButton()
    {
        //continue 버튼 클릭 시.
        //불러오기 기능
    }

    public void ClickSettingButton()
    {
        //setting 버튼 클릭 시.
        SettingEnable();
    }

    public void ClickExitButton()
    {
        //Exit 버튼 클릭 시.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void SettingCloseButton()
    {
        //setting의 x버튼 클릭 시.
        SettingDisable();
    }
}
