using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamEnemySight : MonoBehaviour
{
    public bool recognize;
    BeamEnemy enemy;


    void Start()
    {
        recognize = false;
        enemy = GetComponentInParent<BeamEnemy>();
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

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        *//*if (collision.gameObject.CompareTag("Player") && enemy.canAttack)*//*
        {
            enemy.Recognize();
            *//*recognize = true;*//*
        }
    }*/

    /*private void OnTriggerEnter2D(Collider2D collision)
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
    }*/
}
