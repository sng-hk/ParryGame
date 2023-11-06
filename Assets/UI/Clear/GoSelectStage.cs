using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoSelectStage : MonoBehaviour
{
    public void ClickSelectStage()
    {
        SceneManager.LoadScene("StageSelect");
        Pause.ResumeGame();
        PlayerController.instance.player_helth_point = PlayerController.instance.player_max_helth_point;
        Resources.UnloadUnusedAssets();
    }
}