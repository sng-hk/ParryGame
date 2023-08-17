using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChecker : MonoBehaviour
{
    BoxCollider2D coll;
    PlayerController _player;
    PlayerInventory _player_inventory;

    public SoundManager sound_manager;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = null;
            _player_inventory = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = PlayerController.instance;
            _player_inventory = collision.GetComponent<PlayerInventory>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _player != null && _player_inventory != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (_player_inventory.NumberOfKeyItems >= 1)
                {
                    sound_manager.SfxPlayer(SoundManager.sfx.door_open);
                    Debug.Log("Door Open");
                    _player_inventory.KeyItemUsed();
                    transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("Key is required");
                }
            }
        }
        else
        {
            Debug.Log("null exception or collision is not player");
        }
    }

}
