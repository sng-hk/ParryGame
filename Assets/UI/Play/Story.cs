using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    [SerializeField]
    private GameObject cut_scene;

    [SerializeField]
    private GameObject cut_scene_image;

    public Image standing;
    public Text script;
    public Sprite[] image;
    public string[] text;

    [SerializeField]
    int index = 0;
    int max_index = 10;

    void Start()
    {
        cut_scene.SetActive(true);
        Pause.PauseGame();
        script.text = text[0];
        index = 0;
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
                cut_scene.SetActive(false);
                Pause.ResumeGame();
            }
            else
            {
                SetIamgeAndText(index);
            }

        }
    }

    public void SetIamgeAndText(int index)
    {
        switch (index)
        {
            //½ºÅ×ÀÌÁö ½ÃÀÛ
            case 0:
                standing.sprite = image[2];
                script.text = text[index];
                break;
            //ÁÖÀ§ µÑ·¯º¼ ¶§
            case 1:
            case 2:
                script.text = text[index];
                break;
            //±â¹¦ÇÑ ´À³¦
            case 3:
                standing.sprite = image[1];
                script.text = text[index];
                break;
            case 4:
                standing.sprite = image[0];
                script.text = text[index];
                break;
            //ÄÆ¾À ÄÑ±â
            case 5:
                cut_scene_image.SetActive(true);
                script.text = text[index];
                break;
            case 6:
                script.text = text[index];
                break;
            //´Ù ²ô±â
            case 7:
                cut_scene_image.SetActive(false);
                cut_scene.SetActive(false);
                Pause.ResumeGame();
                break;
        }
    }



}
