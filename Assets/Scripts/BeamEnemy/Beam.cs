using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public Vector3 start_point;
    public Vector3 end_point;
    public Vector3 direction;

    public float speed;
    public float lifetime;

    public bool is_reflect;

    DangerLine danger_line;

    private void Start()
    {
        speed = 4.0f;
        lifetime = 3.0f;

        is_reflect = false;

        danger_line = GetComponent<DangerLine>();
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        direction = end_point - start_point;
        if (is_reflect == false)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else if (is_reflect == true)
        {
            transform.Translate(-direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            if (is_reflect == false)
            {
                speed *= 2;
                is_reflect = true;
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if (is_reflect == true)
            {
                BeamEnemy enemy_take_damage = collision.gameObject.GetComponent<BeamEnemy>();
                enemy_take_damage.TakeDamage(10);
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            if(is_reflect == false)
            {
                PlayerController.instance.player_helth_point -= 2;
            }
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
