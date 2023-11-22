using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public bool recognize = false;
    public Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        if (enemy == null)
        {
            enemy = GetComponentInParent<Boss>();
        }
    }
    
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && enemy.canAttack)
        {
            if (enemy is Boss)
            {
                ((Boss)enemy).Recognize();
            }
            else if (enemy is BeamEnemy)
            {
                ((BeamEnemy)enemy).Recognize();
            }
            else if (enemy is FlyingEmeny)
            {
                ((FlyingEmeny)enemy).Recognize();
            }
            else
            {
                enemy.Recognize();
            }
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
