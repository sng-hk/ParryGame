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
            // �� ��ġ ��� ��������
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
            //������ �߻� ���� �߰� �ʿ�
            is_reflect = true;
            StartCoroutine(nameof(TimeFreeze), 0.4f);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            //������ �Ҹ� �߰� �ʿ�
            sound_manager.SfxPlayer(SoundManager.sfx.attacked);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            // �� �ǰݽ� ����
            if (is_reflect)
            {
                sound_manager.SfxPlayer(SoundManager.sfx.attacked);
                //Enemy���� �Լ� �����ͼ� �����Ű��
                FlyingEmeny enemy_take_damage = collision.gameObject.GetComponent<FlyingEmeny>();
                enemy_take_damage.TakeDamage(10);
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾� �ǰݽ� ����
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
