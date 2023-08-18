using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public static PlayerController instance { get; set; }

    public bool player_in;
    public GameObject exit_portal;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player_in = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player_in = true;
        }
    }

    public void IntoThePortal()
    {
        PlayerController.instance.transform.position = exit_portal.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && player_in == true)
        {
            IntoThePortal();
        }
    }
}
