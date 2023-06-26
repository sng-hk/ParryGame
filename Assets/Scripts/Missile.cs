using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : Bullet
{
    [SerializeField] private GameObject parrySucceed;
    public Transform target;
    private Rigidbody2D rb;

    public float rotateSpeed = 200f;
    private bool inHomingZone = false;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.instance.transform;
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("target =" + this.target.name + ", targetTag = " + this.target.tag);
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void FixedUpdate()
    {
        // '미사일'에서부터 '타겟'까지의 방향벡터
        Vector2 direction;
        if (!inHomingZone || this.target.tag == "Enemy")
        {
            direction = (Vector2)target.position - rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = (-1) * rotateAmount * rotateSpeed;
        }
        else if (inHomingZone && this.target.name == "Body")
        {
            rb.angularVelocity = 0f;
        }
            rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shield") && this.target.name == "Body")
        {
            Instantiate(parrySucceed, transform.position, Quaternion.identity);
            // target 만 Enemy 로 변경
            target = GameObject.FindGameObjectWithTag("Enemy").transform;
            bulletSr.color = afterReflectColor;
            isReflect = true;
            StartCoroutine(nameof(TimeFreeze), 2f);
        }
        else if (collision.gameObject.CompareTag("NonHomingZone"))
        {
            inHomingZone = true;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            Destroy(gameObject);
            // 적 피격시 로직
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            // 플레이어 피격시 로직
        }
    }
}
