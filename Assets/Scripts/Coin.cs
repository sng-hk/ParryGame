using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public SoundManager sound_manager;
    public int coin_number = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sound_manager.SfxPlayer(SoundManager.sfx.get);
            PlayerInventory playerInventory = collision.GetComponent<PlayerInventory>();
            playerInventory.AddCoin(coin_number - 1);
            gameObject.SetActive(false);
        }
    }
}
