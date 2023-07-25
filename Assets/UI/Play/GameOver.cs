using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private Image Heart;

    [SerializeField]
    private Image Heart2;

    [SerializeField]
    private Image Heart3;

    [SerializeField]
    private GameObject gameover_object;

    public void GameoverEnable()
    {
        //게임오버 화면 켜기.
        gameover_object.SetActive(true);
    }

    public void GameoverDisable()
    {
        //게임오버 화면 끄기.
        gameover_object.SetActive(false);
    }

    void Start()
    {
        Heart.fillAmount = 1;
        Heart2.fillAmount = 1;
        Heart3.fillAmount = 1;
        GameoverDisable();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.player_helth_point <= 6 && PlayerController.instance.player_helth_point >= 5)
        {
            Heart3.fillAmount = (PlayerController.instance.player_helth_point - 4) / 2;
        }
        else if (PlayerController.instance.player_helth_point <= 4 && PlayerController.instance.player_helth_point >= 3)
        {
            Heart3.fillAmount = 0;
            Heart2.fillAmount = (PlayerController.instance.player_helth_point - 2) / 2;
        }
        else if (PlayerController.instance.player_helth_point <= 2 && PlayerController.instance.player_helth_point >= 1)
        {
            Heart3.fillAmount = 0;
            Heart2.fillAmount = 0;
            Heart.fillAmount = PlayerController.instance.player_helth_point / 2;
        }
        else
        {
            Heart3.fillAmount = 0;
            Heart2.fillAmount = 0;
            Heart.fillAmount = 0;
            gameover_object.SetActive(true);
            Pause.PauseGame();
        }
    }
}
