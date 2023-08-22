using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatformCheck : MonoBehaviour
{    
    private BoxCollider2D playerCollider;
    private BoxCollider2D platformCollider;
    private bool _is_disable_collision = false;

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

        if(!_is_disable_collision)
        {
            if (PlayerController.instance.transform.position.y <= gameObject.transform.position.y)
            {
                gameObject.layer = LayerMask.NameToLayer("Default");
            }
            else
            {                
                gameObject.layer = LayerMask.NameToLayer("Ground");
            }
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
        gameObject.layer = LayerMask.NameToLayer("Default");
        _is_disable_collision = true;
        yield return new WaitForSeconds(0.3f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        _is_disable_collision = false;
    }
}
