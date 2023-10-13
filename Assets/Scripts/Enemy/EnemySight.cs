using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public static bool recognize = false;
    Enemy parent_script;

    // Start is called before the first frame update
    void Start()
    {
        parent_script = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            parent_script.Recognize();
            recognize = true;
        }
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.transform.position.x > parent_script.transform.position.x)
            {
                parent_script.isFacingRight = true;
            }
            else
            {
                parent_script.isFacingRight = false;
            }
        }
    }*/

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            parent_script.UnRecognize();
            recognize = false;
        }
    }
}
