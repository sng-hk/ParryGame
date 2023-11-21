using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamPortal : MonoBehaviour
{
    public bool player_in = false;
    public bool moving = false;
    public Transform exit_portal;
    public float duration = 1.0f; // 이동에 걸리는 시간

    PlayerController player;

    private Vector3 initial_position;
    private Vector3 target_position;
    private float elapsed_time = 0.0f;

    public SoundManager sound_manager;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player_in = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player_in = true;
        }
    }

    public void IntoThePortal()
    {
        sound_manager.SfxPlayer(SoundManager.sfx.stream_portal);
        initial_position = player.transform.position;
        target_position = exit_portal.position;
        elapsed_time = 0.0f;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && player_in == true)
        {
            moving = true;
            IntoThePortal();
        }

        if (elapsed_time < duration && moving == true)
        {
            elapsed_time += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed_time / duration);
            player.transform.position = Vector3.Lerp(initial_position, target_position, t);
        }
        else
        {
            moving = false;
        }
    }
}
