using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public static int coins = 0;

    [SerializeField]
    private GameObject setting_object;
    public Text setting_sound_value_text;
    //public Text coin_text;

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
        Screen.SetResolution(1920, 1080, true);
        SettingDisable();
        //coin_text.text = coins.ToString();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //esc누르면 세팅창 닫히도록.
            SettingDisable();
        }
    }

    public void ClickNewGameButton()
    {
        //New Game 버튼 클릭 시.
        SceneManager.LoadScene("StageSelect");
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
