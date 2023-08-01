using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public float speed;
    public bool is_left = true;

    void Start()
    {
        speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TurningPoint")
        {
            if (is_left)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                is_left = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                is_left = true;
            }
        }
    }
}
