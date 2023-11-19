using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChecker : MonoBehaviour
{
    BoxCollider2D coll;
    PlayerController _player;
    PlayerInventory _player_inventory;
    
    [SerializeField] private Transform KeyCheckerWall;
    public SoundManager sound_manager;
    KeyCode interactionKey;

    [Header("시작점 끝점 지정: 빨간점(startPoint) -> 초록점(endPoint)")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    private bool wallUnLock = false;
    private float currentTime = 0;
    [SerializeField] private float doorOpenDuration = 5f;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        KeyCheckerWall = transform.parent.GetChild(0);
        interactionKey = PlayerController.instance.interactionKey;
    }

    private void Update()
    {
        if (!wallUnLock)
            return;
        currentTime += Time.deltaTime;
        if(currentTime >= doorOpenDuration)
        {
            currentTime = doorOpenDuration;
        }

        float t = currentTime / doorOpenDuration;

        /*KeyCheckerWall.transform.position += Vector3.right * t;*/

        KeyCheckerWall.position = Vector3.Lerp(startPoint.position, endPoint.position, t);
    }

    private void MoveWall()
    {
        StartCoroutine(MoveWallDelay());        
    }

    IEnumerator MoveWallDelay()
    {
        _player_inventory.KeyItemUsed();
        yield return new WaitForSeconds(1.5f);
        wallUnLock = true;
        yield return new WaitForSeconds(doorOpenDuration);
        transform.parent.gameObject.SetActive(false);
        /*Destroy(transform.parent);*/
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
            if (Input.GetKeyDown(interactionKey))
            {
                if (_player_inventory.NumberOfKeyItems >= 1)
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
