using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReGame : MonoBehaviour
{
    public void ClickReGame()
    {
        GoStage.loding_scene = SceneManager.GetActiveScene().name;
        
        Pause.ResumeGame();
        PlayerController.instance.player_helth_point = PlayerController.instance.player_max_helth_point;
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene("loading");
    }
}
