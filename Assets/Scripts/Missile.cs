using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : Bullet
{
    [SerializeField] private GameObject parrySucceed;
    public Transform target;
    [SerializeField] private Vector2 _target_offset;
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
        // '�̻���'�������� 'Ÿ��'������ ���⺤��
        Vector2 direction;
        if (!inHomingZone || this.target.tag == "Enemy")
        {
            direction = (Vector2)target.position + _target_offset - rb.position;
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
            // target �� Enemy �� ����
            target = GameObject.FindGameObjectWithTag("Enemy").transform;
            bulletSr.color = afterReflectColor;
            isReflect = true;
            StartCoroutine(nameof(TimeFreeze), 0.7f);
        }
        else if (collision.gameObject.CompareTag("NonHomingZone"))
        {
            inHomingZone = true;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            // �� �ǰݽ� ����
            if (isReflect)
            {
                Enemy enemy_take_damage = collision.gameObject.GetComponent<Enemy>();
                enemy_take_damage.TakeDamage(10);
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾� �ǰݽ� ����
            if (!isReflect)
            {
                PlayerController.instance.player_helth_point -= 1;
                Destroy(gameObject);
            }
        }
    }
}
