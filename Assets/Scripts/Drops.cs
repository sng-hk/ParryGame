using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    public bool is_reflect;
    private FlyingEmeny shooter;
    private Rigidbody2D rb;
    Vector3 shooter_position;
    [SerializeField] private Vector2 _target_offset;
    [Header("Speed")]
    public float speed = 80.0f;

    public SoundManager sound_manager;

    private void Awake()
    {
        sound_manager = FindObjectOfType<SoundManager>();
    }

    public void MemoryShooter(FlyingEmeny enemy)
    {
        shooter = enemy;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (shooter != null)
        {
            // 적 위치 계속 가져오기
            shooter_position = shooter.transform.position;
        }

        if (is_reflect == true)
        {
            Vector3 direction = (shooter_position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            sound_manager.SfxPlayer(SoundManager.sfx.parrying);
            //적에게 발사 구문 추가 필요
            is_reflect = true;
            StartCoroutine(nameof(TimeFreeze), 0.4f);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            //터지는 소리 추가 필요
            sound_manager.SfxPlayer(SoundManager.sfx.attacked);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            // 적 피격시 로직
            if (is_reflect)
            {
                sound_manager.SfxPlayer(SoundManager.sfx.attacked);
                //Enemy에서 함수 가져와서 적용시키기
                FlyingEmeny enemy_take_damage = collision.gameObject.GetComponent<FlyingEmeny>();
                enemy_take_damage.TakeDamage(10);
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어 피격시 로직
            if (!is_reflect)
            {
                sound_manager.SfxPlayer(SoundManager.sfx.attacked);
                PlayerController.instance.player_helth_point -= 1;
                Destroy(gameObject);
            }
        }
    }

    protected IEnumerator TimeFreeze(float freezingTime)
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSecondsRealtime(freezingTime);
        Time.timeScale = 1f;
    }
}
