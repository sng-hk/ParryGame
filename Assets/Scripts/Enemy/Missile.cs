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
    public float speed_afterParry = 10.0f;

    private Enemy shooter;
    Vector3 shooter_position;

    public void MemoryShooter(Enemy enemy)
    {
        shooter = enemy;
    }

    void Start()
    {
        target = PlayerController.instance.transform;
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("target =" + this.target.name + ", targetTag = " + this.target.tag);
    }

    void Update()
    {
        if (shooter != null)
        {
            // 적 위치 계속 가져오기
            shooter_position = shooter.transform.position;
        }
    }


    private void FixedUpdate()
    {
        // '미사일'에서부터 '타겟'까지의 방향벡터
        Vector2 direction;
        if (!inHomingZone)
        {
            if(isReflect == true)
            {
                direction = (Vector2)shooter_position + _target_offset - rb.position;
            }
            else
            {
                direction = (Vector2)target.position + _target_offset - rb.position;
            }
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.right).z;
            rb.angularVelocity = (-1) * rotateAmount * rotateSpeed;
        }
        else if (inHomingZone && this.target.name == "Body")
        {
            rb.angularVelocity = 0f;
        }
            rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shield") && this.target.name == "Body")
        {
            sound_manager.SfxPlayer(SoundManager.sfx.parrying);
            Instantiate(parrySucceed, transform.position, Quaternion.identity);
            // target 을 Enemy 로 변경
            speed *= 2;
            target = GameObject.FindGameObjectWithTag("Enemy").transform;
            bulletSr.color = afterReflectColor;
            isReflect = true;
            StartCoroutine(nameof(TimeFreeze), 0.3f);
        }
        else if (collision.gameObject.CompareTag("NonHomingZone"))
        {
            inHomingZone = true;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            // 적 피격시 로직
            if (isReflect)
            {
                sound_manager.SfxPlayer(SoundManager.sfx.attacked);
                //Enemy에서 함수 가져와서 적용시키기
                Enemy enemy_take_damage = collision.gameObject.GetComponent<Enemy>();
                enemy_take_damage.TakeDamage(10);
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어 피격시 로직
            if (!isReflect)
            {
                sound_manager.SfxPlayer(SoundManager.sfx.attacked);
                PlayerController.instance.player_helth_point -= 1;
                Destroy(gameObject);
            }
        }
    }
}
