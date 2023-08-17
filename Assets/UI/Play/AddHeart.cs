using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHeart : MonoBehaviour
{
    public SoundManager sound_manager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sound_manager.SfxPlayer(SoundManager.sfx.get);
            PlayerController.instance.player_max_helth_point += 2;
            PlayerController.instance.player_helth_point += 2;
            gameObject.SetActive(false);
        }
    }
}
