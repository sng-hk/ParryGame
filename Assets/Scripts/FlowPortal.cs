using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowPortal : MonoBehaviour
{
    public bool player_in = false;
    public bool active = false;

    public float speed; // speed of the platform
    public int starting_point; // starting index (position of the platform)
    public Transform[] points; // an array of transform points

    [SerializeField] private int i = 0; // index of the array

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collison");
        if (collision.gameObject.CompareTag("Player"))
        {
            player_in = true;
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("UnCollison");
        if (collision.gameObject.CompareTag("Player"))
        {
            player_in = false;
            collision.transform.SetParent(null);
            transform.position = points[starting_point].position;
        }
    }
    public void Active()
    {
        Debug.Log("Active");
        if (active == true)
        {
            active = false;
        }
        if (active == false)
        {
            active = true;
        }
    }

    void Start()
    {
        Debug.Log("Start");
        speed = 4;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && player_in == true)
        {
            Active();
        }

        if (Vector2.Distance(transform.position, points[i].position) < 0.02f && active == true) // check if the platform was on the olast point after the increase
        {
            i++;
            if (i == points.Length)
            {
                i = 0; // reset the index
                Active();
            }
        }

        if (active == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
    }
}
