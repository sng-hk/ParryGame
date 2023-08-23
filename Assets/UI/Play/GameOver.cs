using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Image[] hearts;
    public Image[] black_hearts;

    [SerializeField]
    private GameObject _gameover_object;

    public float player_heart_counter;

    public SoundManager sound_manager;
    public void GameoverEnable()
    {
        sound_manager.SfxPlayer(SoundManager.sfx.game_over);
        //게임오버 화면 켜기.
        _gameover_object.SetActive(true);
    }

    public void GameoverDisable()
    {
        //게임오버 화면 끄기.
        _gameover_object.SetActive(false);
    }

    void Start()
    {
        GameoverDisable();

        player_heart_counter = PlayerController.instance.player_max_helth_point / 2;
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].fillAmount = 1;
            if (i + 1 > player_heart_counter)
            {
                hearts[i].enabled = false;
                black_hearts[i].enabled = false;
            }
        }
    }

    void Update()
    {
        player_heart_counter = PlayerController.instance.player_max_helth_point / 2;

        if (PlayerController.instance.player_helth_point <= 0)
        {
            /*Pause.PauseGame();
            GameoverEnable();*/
            PlayerController.instance.StartCoroutine("Respawn");
        }

        for (int i = 3; i < hearts.Length; i++)
        {
            if (i + 1 <= player_heart_counter)
            {
                hearts[i].enabled = true;
                black_hearts[i].enabled = true;
            }
        }

        for (int i = 0; i < player_heart_counter; i++)
        {
            if (i + 1 <= PlayerController.instance.player_helth_point / 2)
            {
                hearts[i].fillAmount = 1;
            }
            else if(i < PlayerController.instance.player_helth_point / 2 && i + 1 > PlayerController.instance.player_helth_point / 2)
            {
                hearts[i].fillAmount = 0.5f;
            }
            else
            {
                hearts[i].fillAmount = 0;
            }
        }
    }
}
