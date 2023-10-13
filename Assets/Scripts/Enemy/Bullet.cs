using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Speed")]
    public float speed = 5.0f;    

    [Header("Color")]
    public Color defaultColor = new Color(1.0f, 0f, 0f, 1f);
    public Color afterReflectColor = new Color(0f, 1.0f, 0f, 1f);
    private Color halfalphaColor = new Color(1, 1, 1, 0.6f);
    private Color fullalphaColor = new Color(1, 1, 1, 1);

    [Header("Sprite Rederer")]
    public SpriteRenderer bulletSr;

    [SerializeField]
    public int direction;
    public bool isReflect;

    public SoundManager sound_manager;

    private void Awake()
    {
        sound_manager = FindObjectOfType<SoundManager>();
        StartCoroutine(DestroyAfterSeconds());
    }

    void Start()
    {
        bulletSr = GetComponent<SpriteRenderer>();
        bulletSr.color = defaultColor;
        direction = 1;
        isReflect = false;
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime * direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �и� ���� �� ����
        if (collision.gameObject.CompareTag("Shield"))
        {
            Debug.Log("sheild parry");
            // �߰� ��������: �� �ڵ� ����
            direction *= -1;
            speed *= 4;
            bulletSr.color = afterReflectColor;
            isReflect = true;

        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isReflect == true)
            {
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.instance.player_helth_point -= 1;
            Destroy(gameObject);
        }
    }

    protected IEnumerator TimeFreeze(float freezingTime)
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSecondsRealtime(freezingTime);
        Time.timeScale = 1f;
    }

    IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(4.0f);
        if (isReflect)
        {
            yield return new WaitForSeconds(5.0f);
        }
        Destroy(gameObject);
    }
}
