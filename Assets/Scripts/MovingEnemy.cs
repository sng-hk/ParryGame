using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public float speed;
    public bool is_left = true;

    public Transform player;
    public Transform enemy;
    public float player_x;
    public float enemy_x;

    void Start()
    {
        speed = 2;
        player = PlayerController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        player_x = player.position.x;
        enemy_x = transform.position.x;

        if(enemy_x > player_x)
        {
            if (!is_left)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                is_left = true;
            }
        }
        else if(enemy_x < player_x)
        {
            if (is_left)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                is_left = false;
            }
        }
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
