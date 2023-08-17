using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public SoundManager sound_manager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sound_manager.SfxPlayer(SoundManager.sfx.get);
        }
        PlayerInventory playerInventory = collision.GetComponent<PlayerInventory>();

        if (playerInventory != null)
        {
            playerInventory.KeyItemCollected();
            gameObject.SetActive(false);
        }
    }
}
