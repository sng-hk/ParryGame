using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public static bool recognize = false;
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && enemy.canAttack)
        {
            enemy.Recognize();
            /*recognize = true;*/
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

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.UnRecognize();
            recognize = false;
        }
    }*/
}
