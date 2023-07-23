using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChecker : MonoBehaviour
{
    BoxCollider2D coll;
    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        PlayerInventory playerInventory = collision.gameObject.GetComponent<PlayerInventory>();
        if (player != null && playerInventory != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (playerInventory.NumberOfKeyItems >= 1)
                {
                    Debug.Log("Door Open");
                    transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("Key is required");
                }
            }
        }
        else
        {
            Debug.Log("null exception");
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        PlayerInventory playerInventory = collision.GetComponent<PlayerInventory>();
        if (player != null && playerInventory != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (playerInventory.NumberOfKeyItems >= 1)
                {
                    Debug.Log("Door Open");
                    transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("Key is required");
                }
            }
        }
        else
        {
            Debug.Log("null exception");
        }

    }*/

}
