using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChecker : MonoBehaviour
{
    BoxCollider2D coll;
    PlayerController _player;
    PlayerInventory _player_inventory;
    
    private GameObject KeyCheckerWall;
    public SoundManager sound_manager;

    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;

    private bool wallUnLock = false;
    private float currentTime = 0;
    [SerializeField] private float lerpTime = 0.5f;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        KeyCheckerWall = transform.parent.gameObject;        
    }

    private void Update()
    {
        if (!wallUnLock)
            return;
        currentTime += Time.deltaTime;
        if(currentTime >= lerpTime)
        {
            currentTime = lerpTime;
        }

        float t = currentTime / lerpTime;

        KeyCheckerWall.transform.position = Vector3.Lerp(startPosition.position, endPosition.position, t);
    }

    private void MoveWall()
    {
        StartCoroutine(MoveWallDelay());        
    }

    IEnumerator MoveWallDelay()
    {
        _player_inventory.KeyItemUsed();
        yield return new WaitForSeconds(0.7f);
        wallUnLock = true;
        yield return new WaitForSeconds(0.7f);
        /*transform.parent.gameObject.SetActive(false);*/
        Destroy(transform.parent);
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
                if (_player_inventory.NumberOfKeyItems >= 0)
                {
                    /*sound_manager.SfxPlayer(SoundManager.sfx.door_open);*/
                    Debug.Log("Door Open");
                    MoveWall();                    
                }
                else
                {
                    Debug.Log("Key is required");
                }
            }
        }
        /*else
        {
            Debug.Log("null exception or collision is not player");
        }*/
    }

}
