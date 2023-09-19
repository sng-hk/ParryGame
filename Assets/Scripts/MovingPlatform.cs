using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed; // speed of the platform
    public int startingPoint; // starting index (position of the platform)
    public Transform[] points; // an array of transform points

    [SerializeField] private int i; // index of the array
    [SerializeField] private int id;
    [SerializeField] private Vector3 offset;
    void Start()
    {
        transform.position = points[startingPoint].position + offset; // setting the position of the platform to
        // the position of one of the points using index "startingPoint"        s
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position + offset) < 0.02f) // check if the platform was on the olast point after the increase
        {
            i++;
            if (i == points.Length)
            {
                i = 0; // reset the index
            }
        }

        // moving the platform to the point position with the index "i"
        transform.position = Vector2.MoveTowards(transform.position, points[i].position + offset, speed * Time.deltaTime);
        /*transform.position = Vector3.Lerp();*/
        /*t = t * t * t * (t * (6f * t - 15f) + 10f);*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.position.y < collision.transform.position.y)
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
