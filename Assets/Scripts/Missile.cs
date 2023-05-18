using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : Bullet
{
    public Transform target;
    private Rigidbody2D rb;
    
    public float rotateSpeed = 200f;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.instance.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        // '미사일'에서부터 '타겟'까지의 방향벡터
        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = (-1)*rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            // target 만 Enemy 로 변경
            Debug.Log("sheild parry");
            target = GameObject.FindGameObjectWithTag("Enemy").transform;
            bulletSr.color = afterReflectColor;
            isReflect = true;            
            StartCoroutine(nameof(TimeFreeze), 0.5f);
        }
        else
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                Destroy(gameObject);
            }
            else if (collision.gameObject.CompareTag("Player"))
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            }
        }
    }
}
