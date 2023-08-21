using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoStage : MonoBehaviour
{
    public static string loding_scene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ClickGoStageButton()
    {
        loding_scene = "SampleScene";
        SceneManager.LoadScene("loading");
    }
    public void ClickTutorialStageButton()
    {
        loding_scene = "TutorialScene";
        SceneManager.LoadScene("loading");
    }

    public void ClickStage1Button()
    {
        loding_scene = "Stage1";
        SceneManager.LoadScene("loading");
    }
    public void ClickStage2Button()
    {
        SceneManager.LoadScene("loading");
    }
}
