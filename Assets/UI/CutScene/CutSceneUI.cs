using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CutSceneUI : MonoBehaviour
{
    [SerializeField]
    private GameObject setting_object;
    public Text setting_sound_value_text;

    bool is_on = false;

    public Image cut_scene;
    public Text script;

    public Sprite[] image;
    public string[] text;

    [SerializeField]
    int index = 0;
    int max_index = 13;

    public SoundManager sound_manager;

    // Start is called before the first frame update
    void Start()
    {
        SettingDisable();
        cut_scene.sprite = image[0];
        script.text = text[0];
        index = 0;
        Screen.SetResolution(1920, 1080, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            index++;
            if (index == max_index)
            {
                index = 0;
                GoTutorialButton();
            }
            else
            {
                SetIamgeAndText(index);
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(is_on == true)
            {
                is_on = false;
                SettingDisable();
            }
            else
            {
                is_on = true;
                SettingEnable();
            }
           
        }
    }

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

    public void SetIamgeAndText(int index)
    {
        switch (index)
        {
            //검은 화면
            case 0:
                cut_scene.sprite = image[0];
                script.text = text[index];
                break;
            case 1:
                script.text = text[index];
                break;
            //불사자들
            case 2:
                sound_manager.SfxPlayer(SoundManager.sfx.paper_turn);
                cut_scene.sprite = image[1];
                script.text = text[index];
                break;
            case 3:
            case 4:
            case 5:
                script.text = text[index];
                break;
            //일곱 무구
            case 6:
                sound_manager.SfxPlayer(SoundManager.sfx.paper_turn);
                cut_scene.sprite = image[2];
                script.text = text[index];
                break;
            case 7:
            case 8:
                script.text = text[index];
                break;
            //갇히는 불사자들의 영혼
            case 9:
                sound_manager.SfxPlayer(SoundManager.sfx.paper_turn);
                cut_scene.sprite = image[3];
                script.text = text[index];
                break;
            case 10:
                script.text = text[index];
                break;
            //검은 화면
            case 11:
                sound_manager.SfxPlayer(SoundManager.sfx.paper_turn);
                cut_scene.sprite = image[4];
                script.text = text[index];
                break;
            case 12:
                script.text = text[index];
                break;



        }
    }

    public void GoTutorialButton()
    {
        SceneManager.LoadScene("TutorialR");
        Resources.UnloadUnusedAssets();
    }

}
