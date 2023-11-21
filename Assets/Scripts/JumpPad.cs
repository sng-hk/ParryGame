using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private float bounce = 20f;
    public SoundManager sound_manager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            sound_manager.SfxPlayer(SoundManager.sfx.jump_pad);
            Rigidbody2D player_rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (Input.GetKey(KeyCode.X))
            {
                player_rb.AddForce(Vector2.up * (bounce - PlayerController.instance.jumpForce), ForceMode2D.Impulse);
            }
            else
            {
            player_rb.AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            }
                
        }
    }
}
