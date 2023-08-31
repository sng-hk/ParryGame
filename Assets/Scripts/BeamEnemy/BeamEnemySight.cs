using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamEnemySight : MonoBehaviour
{
    public bool recognize;
    BeamEnemy parent_script;


    void Start()
    {
        recognize = false;
        parent_script = GetComponentInParent<BeamEnemy>();
    }

    void Update()
    {
        if(recognize == true)
        {
            Debug.Log("player in");
        }
        else if(recognize == false)
        {
            Debug.Log("player out");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            parent_script.Recognize();
            recognize = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            parent_script.UnRecognize();
            recognize = false;
        }
    }
}
