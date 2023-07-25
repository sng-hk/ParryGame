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
        SettingDisable();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //esc������ ����â ��������.
            SettingDisable();
        }
    }

    public void TextChanger(float value)
    {
        //���� ũ�⿡ ���� ���ϴ� �ؽ�Ʈ.
        sound_value = (int)(value*100);
        setting_sound_value_text.text = sound_value.ToString();
    }

    public void ClickNewGameButton()
    {
        //New Game ��ư Ŭ�� ��.
        SceneManager.LoadScene("loading");
    }

    public void ClickContinueButton()
    {
        //continue ��ư Ŭ�� ��.
        //�ҷ����� ���
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
