using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoSelectStage : MonoBehaviour
{
    private PlayerInventory inventory;

    public void ClickSelectStage()
    {
        Pause.ResumeGame();
        SceneManager.LoadScene("StageSelect");
        PlayerController.instance.player_helth_point = PlayerController.instance.player_max_helth_point;
        inventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        /*MainMenu.coins += inventory.Coins();
        Resources.UnloadUnusedAssets();*/
    }

    public void GoCutSceneStage()
    {
        Pause.ResumeGame();
        SceneManager.LoadScene("CutScene2");
        PlayerController.instance.player_helth_point = PlayerController.instance.player_max_helth_point;
        inventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        /*MainMenu.coins += inventory.Coins();
        Resources.UnloadUnusedAssets();*/
    }
}