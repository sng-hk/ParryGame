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
        //����â �ѱ�.
        setting_object.SetActive(true);
    }

    public void SettingDisable()
    {
        //����â ����.
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
            //esc������ ����â ��������.
            SettingDisable();
        }
    }

    public void ClickNewGameButton()
    {
        //New Game ��ư Ŭ�� ��.
        SceneManager.LoadScene("StageSelect");
    }

    public void ClickSettingButton()
    {
        //setting ��ư Ŭ�� ��.
        SettingEnable();
    }

    public void ClickExitButton()
    {
        //Exit ��ư Ŭ�� ��.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void SettingCloseButton()
    {
        //setting�� x��ư Ŭ�� ��.
        SettingDisable();
    }
}
