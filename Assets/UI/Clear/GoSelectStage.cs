using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoSelectStage : MonoBehaviour
{
    [SerializeField]
    private GameObject clear_object;

    public void ClickSelectStage()
    {
        Pause.ResumeGame();
        SceneManager.LoadScene("StageSelect");
        PlayerController.instance.player_helth_point = PlayerController.instance.player_max_helth_point;
        Resources.UnloadUnusedAssets();
    }
}