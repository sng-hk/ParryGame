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
        loding_scene = "TutorialR";
        SceneManager.LoadScene("loading");
    }

    public void ClickStage1Button()
    {
        loding_scene = "Stage1r";
        SceneManager.LoadScene("loading");
    }
    public void ClickStage2Button()
    {
        loding_scene = "Stage2r";
        SceneManager.LoadScene("loading");
    }
    public void ClickStage3Button()
    {
        loding_scene = "Stage3r";
        SceneManager.LoadScene("loading");
    }
}
