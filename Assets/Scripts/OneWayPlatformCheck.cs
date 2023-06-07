using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatformCheck : MonoBehaviour
{
    private GameObject currentOneWayPlatform;

    [SerializeField] private BoxCollider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log(currentOneWayPlatform);
            if (currentOneWayPlatform != null)
            {                
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        Debug.Log("Disable");
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        Debug.Log("Able");
    }
}
