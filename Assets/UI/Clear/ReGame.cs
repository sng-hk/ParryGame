using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReGame : MonoBehaviour
{
    public void ClickReGame()
    {
        Pause.ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerController.instance.player_helth_point = PlayerController.instance.player_max_helth_point;
        Resources.UnloadUnusedAssets();

    }
}
