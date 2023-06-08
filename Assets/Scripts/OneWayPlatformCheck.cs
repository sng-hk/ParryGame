using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatformCheck : MonoBehaviour
{    
    private BoxCollider2D playerCollider;
    private BoxCollider2D platformCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (platformCollider != null)
                StartCoroutine(DisableCollision());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            Debug.Log("On OneWayPlatform");
            platformCollider = collision.gameObject.GetComponent<BoxCollider2D>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            Debug.Log("Out OneWayPlatform");
            /*platformCollider = null;*/
        }
    }

    private IEnumerator DisableCollision()
    {
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        PlayerController.instance.RB.gravityScale = PlayerController.instance.gravity;        
        PlayerController.instance.RB.velocity = new Vector2(PlayerController.instance.RB.velocity.x, PlayerController.instance.maxFallSpeed);
        yield return new WaitForSeconds(0.05f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}
